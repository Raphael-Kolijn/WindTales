﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadBlowInput : MonoBehaviour {
    [SerializeField]
    private DeviceManager.DeviceType deviceType;
    double flowRate;
	// Use this for initialization
	void Start () {
        DeviceManager.Instance.SetDeviceType(deviceType);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        CalculateFlow();
      //  Debug.Log("flowrate = " + flowRate);
	}

    void CalculateFlow()
    {
        flowRate = DeviceManager.Instance.FlowLMin;
        flowRate = System.Math.Round(flowRate, 1);
    }

    public double getFlow()
    {
        return flowRate;
    }
}
