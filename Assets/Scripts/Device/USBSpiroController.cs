using System.IO;
using System.IO.Ports;
using System.Text;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// SpiroController for the Kueffner spirometer
/// </summary>
public class USBSpiroController : SpiroController
{

    public USBSpiroController(string port)
    {
        portName = port;
    }

    public string portName = ""; //Controller.instance.getPortName();
    private SerialPort port;
    private Thread myThread;

    private double flow = 0;
    private double cleanFlow = 0;
    private const double flowMultiplier = 1.35;

    public USBSpiroController()
    {

    }

    public override void ConnectDevice()
    {
        Debug.Log("Connecting USB Controller");
        //SerialPort p = new SerialPort("\\\\.\\COM17", 9600);
        portName = PlayerPrefs.GetString("portname", portName);
        DisconnectDevice();
        //stop if we are already connected or if the port is unavailable
        if ((port != null && port.IsOpen) || !PortIsAvailable())
        {
            Debug.LogError("Port " + portName + " unavailable.");
            return;
        }
        
        port = new SerialPort("\\\\.\\" + portName);
        port.Encoding = Encoding.Default;
        port.BaudRate = 9600;
        port.ReadBufferSize = 10000;
        port.ReadTimeout = 100;
        port.Handshake = Handshake.None;
        port.NewLine = "\r\n";

        try
        {
            port.Open();
            Debug.Log("Port opened, DEVICE CONNECTED");
        }
        catch (IOException ex)
        {
            Debug.LogError("ERROR - Device not connected, port NOT OPEN!");
            Debug.LogError(ex.ToString());

            return;
        }

        //write to the port to start data transmission
        port.Write("w");
        port.Write("w");        
        myThread = new Thread(GetData);
        myThread.Start();
	
		isConnected = true;
    }

    private bool PortIsAvailable()
    {
        string[] portNames = SerialPort.GetPortNames();
        bool portFound = true;

        foreach (string portNameCurr in portNames)
        {
            if (portNameCurr == portName)
            {
                Debug.Log("Port name correct!");
                portFound = true;
                break;
            }
            else
            {
                Debug.LogError("ERROR - Incorrect port name, port NOT OPEN!");
                portFound = false;
            }
        }

        return portFound;
    }

    public override void UpdateSpiroController()
    {
    }

    private void GetData()
    {
        while (myThread.IsAlive)
        {
           // Debug.Log("Thread alive");
            if (port != null && port.IsOpen)
            {

                try
                {
                    string indata = port.ReadLine();
                    string[] data = indata.Split(new[] { ';' });

                   // Debug.Log(indata);

                    try
                    {
                        cleanFlow = Convert.ToDouble(data[0]);
                        flow = cleanFlow * flowMultiplier;
						DeviceManager.Instance.FlowLMin = (flow / 78.123f);
                    }
                    catch (FormatException ex)
                    {
                        Debug.LogError(ex.Message);
                    }
                }
                catch (TimeoutException ex)
                {
                    Debug.LogError(ex.Message);
                }
            }
        }
    }

    public override void DisconnectDevice()
    {
        Debug.Log("Disconnecting device");

		isConnected = false;
        if (port != null && port.IsOpen)
        {
            port.Close();
            port = null;
        }

        if (myThread != null && myThread.IsAlive)
        {
            myThread.Abort();
            myThread.Join();
            myThread = null;
        }
    }
}
