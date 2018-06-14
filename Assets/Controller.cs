using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Controller : PersistentSingleton<Controller>
{

    public static Controller instance;
    [SerializeField]
    private string portName;


    public string getPortName()
    {
        if (String.IsNullOrEmpty(portName))
        {
            return "COM13";
        }
        return portName;
    }

    public void setPortName(string com)
    {
        if (DeviceManager.deviceType != DeviceManager.DeviceType.KUEFFNER)
        {
            Debug.Log("Devicetype is geen kueffner");
            return;
        }
        if (String.IsNullOrEmpty(com) && DeviceManager.deviceType == DeviceManager.DeviceType.KUEFFNER)
        {
            Debug.Log("Je com poort is leeg");
            return;
        }

        portName = com;
    }
}


