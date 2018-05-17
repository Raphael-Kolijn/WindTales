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
        DeviceManager.Instance.SetDeviceType(DeviceManager.DeviceType.KUEFFNER);
    }
	
	// Update is called once per frame
	void Update ()
    {
        text.text = "Current flow: " + DeviceManager.Instance.FlowLMin + "; Flow state: " + DeviceManager.Instance.CurrentFlowState;
	}
}
