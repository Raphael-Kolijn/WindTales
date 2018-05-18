using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


[RequireComponent(typeof(Text))]
public class SimulatedControllerExample : MonoBehaviour
{

    [SerializeField]
    private DeviceManager.DeviceType deviceType;

    private Text text;

	// Use this for initialization
	void Awake ()
    {
        text = GetComponent<Text>();
        DeviceManager.Instance.SetDeviceType(deviceType);
    }
	
	// Update is called once per frame
	void Update ()
	{

		double flowRate = DeviceManager.Instance.FlowLMin;

		flowRate = Math.Round(flowRate, 1);
        text.text = "Current flow: " + flowRate + "; Flow state: " + DeviceManager.Instance.CurrentFlowState;
	}
}
