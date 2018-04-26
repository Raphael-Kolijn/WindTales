using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// SpiroController for the AIRNEXT spirometer
/// </summary>
public class BLESpiroController : SpiroController
{

    private string _serviceUUID = "00001816-0000-1000-8000-00805f9b34fb";
    private string _readCharacteristicUUID = "00002a5b-0000-1000-8000-00805f9b34fb";
    private string _writeCharacteristicUUID = "00002a5b-0000-1000-8000-00805f9b34fb";
    private string _BatteryserviceUUID = "0000180F-0000-1000-8000-00805f9b34fb";
    private string _BatteryCharacteristicUUID = "00002a19-0000-1000-8000-00805f9b34fb";
    private string deviceToConnectTo = "Air Next";
    private string deviceToConnectTo2 = "Air Next-C75";

    private bool _readFound = false;
    private bool _writeFound = false;
    private string _connectedID = null;
    private Dictionary<string, string> _peripheralList;
    private float _subscribingTimeout = 0f;
    private string adressFinal;
    private double flowLSec = 0.0f;

    public BLESpiroController()
    {

    }

    public override void ConnectDevice()
    {
        BluetoothLEHardwareInterface.Initialize(true, false, () => { },
                                      (error) => { }
        );
        BluetoothLEHardwareInterface.ScanForPeripheralsWithServices(null, (address, name) =>
        {
            if (name.Trim().ToLower() == deviceToConnectTo.Trim().ToLower()|| name.Trim().ToLower() == deviceToConnectTo2.Trim().ToLower())
            {
                if (String.IsNullOrEmpty(PlayerPrefs.GetString("adress")))
                {
                    PlayerPrefs.SetString("adress", address);
                }
            }
            if (PlayerPrefs.GetString("adress")== address)
         {
                PlayerPrefs.SetString("adress", address);
                AddPeripheral(name, address);
        }
        }, (address, name, rssi, advertisingInfo) => { });

    }

    private void AddPeripheral(string name, string address)
    {
        Debug.Log("Found BLE Device " + name + " with address " + address);
        if (_peripheralList == null)
        {
            _peripheralList = new Dictionary<string, string>();
        }
        if (!_peripheralList.ContainsKey(address))
        {
            _peripheralList[address] = name;
            if (name.Trim().ToLower() == deviceToConnectTo.Trim().ToLower() || name.Trim().ToLower() == deviceToConnectTo2.Trim().ToLower())
            {
                Debug.Log("Connecting to BLE Device " + name);
                ConnectDevice(address);
                Debug.Log("Connecting to BLE Device " + name);
            }
        }
        else
        {
            Debug.Log("No BLE Device with address " + address + " found.");
        }
    }

    public override void UpdateSpiroController()
    {
        if (_readFound && _writeFound)
        {
            _readFound = false;
            _writeFound = false;
            _subscribingTimeout = 1.0f;
        }

        if (_subscribingTimeout > 0f)
        {
            _subscribingTimeout -= Time.deltaTime;
            if (_subscribingTimeout <= 0f)
            {
                _subscribingTimeout = 0f;
                BluetoothLEHardwareInterface.SubscribeCharacteristicWithDeviceAddress(
                   _connectedID, _serviceUUID, _readCharacteristicUUID,
                   (deviceAddress, notification) =>
                   {
                   },
                   (deviceAddress2, characteristic, data) =>
                   {
                       isConnected = true;
                       BluetoothLEHardwareInterface.Log("id: " + _connectedID);
                       if (deviceAddress2.CompareTo(_connectedID) == 0)
                       {
                           BluetoothLEHardwareInterface.Log(string.Format("data length: {0}", data.Length));
                           if (data.Length > 0)
                           {
                               double flowMLSec = BitConverter.ToInt16(data, 1);  //raw data represents airflow in milliliters per second
                               flowLSec = flowMLSec / 1000; //converted to flow in liters per second
                               DeviceManager.Instance.FlowLMin = -(flowLSec * 60); //converted to flow in liters per minute
                           }
                       }
                   });
            }
        }
    }

    private void ConnectDevice(string addr)
    {
        BluetoothLEHardwareInterface.ConnectToPeripheral(addr, (address) =>
        {
        },
           (address, serviceUUID) =>
           {
           },
           (address, serviceUUID, characteristicUUID) =>
           {
               if (IsEqual(serviceUUID, _serviceUUID))
               {
                   _connectedID = address;
                   isConnected = true;

                   if (IsEqual(characteristicUUID, _readCharacteristicUUID))
                   {
                       _readFound = true;
                   }
                   if (IsEqual(characteristicUUID, _writeCharacteristicUUID))
                   {
                       _writeFound = true;

                   }
                   adressFinal = addr;
                   Debug.Log("BLE Spiro Connected");
                   BluetoothLEHardwareInterface.StopScan();
               }
           }, (address) =>
           {
               // this will get called when the device disconnects 
               // be aware that this will also get called when the disconnect 
               // is called above. both methods get call for the same action 
               // this is for backwards compatibility 
               Debug.Log("Connection Lost");
               isConnected = false;
               //we first need to disconnect and deinitialize before we can connect again to the Air next otherwise we can't reconnect.
               BluetoothLEHardwareInterface.DisconnectAll();
               BluetoothLEHardwareInterface.DeInitialize(() => { });
               BluetoothLEHardwareInterface.FinishDeInitialize();
               DeviceManager.Instance.MakeSpiroControllerNull();
           });
    }

    public override void DisconnectDevice()
    {
        //need to disconnect and deinitialize and then finish it otherwise it won't be disconnected the right way.
        BluetoothLEHardwareInterface.DisconnectAll();
        ConnectDevice(adressFinal);
        BluetoothLEHardwareInterface.DeInitialize(() => { });
        BluetoothLEHardwareInterface.FinishDeInitialize();
        isConnected = false;
    }

    private bool IsEqual(string uuid1, string uuid2)
    {
        return (uuid1.ToUpper().CompareTo(uuid2.ToUpper()) == 0);
    }

    private ushort ParseData(byte[] data)
    {
        const byte valueFormat = 0x01;

        byte flags = data[0];
        bool isValueSizeLong = ((flags & valueFormat) != 0);

        return BitConverter.ToUInt16(data, 1);
    }
}
