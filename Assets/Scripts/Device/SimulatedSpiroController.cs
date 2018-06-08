using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

/// <summary>
/// SpiroController that turns gamepad input into simulated flow
/// </summary>
public class SimulatedSpiroController : SpiroController
{

    public override void ConnectDevice()
    {
		isConnected = true;
        Debug.Log("Connected simulated spiro");
    }

    public override void DisconnectDevice()
    {
		isConnected = false;
        Debug.Log("Disconnected simulated spiro");
    }

    public override void UpdateSpiroController()
    {
#if (!UNITY_ANDROID && !UNITY_IOS) || UNITY_EDITOR
        if (DeviceManager.Instance.GetDeviceType() == DeviceManager.DeviceType.PSCONTROLLER)
        {
            DeviceManager.Instance.FlowLMin = DeviceManager.Instance.MaxFlowLMin * Input.GetAxis("Player_SimulateBreathingPs4");
           // Debug.Log(DeviceManager.Instance.FlowLMin);
        }
        else
        {
            DeviceManager.Instance.FlowLMin = DeviceManager.Instance.MaxFlowLMin * -Input.GetAxis("Player_SimulateBreathing");
        }
       
#else
            if(Input.GetButton("Player_SimulateBreathingMobile") && Input.GetAxisRaw("Player_SimulateBreathingMobile") > 0)
            {
				DeviceManager.Instance.FlowLMin = DeviceManager.Instance.MaxFlowLMin;
            }
            else if (Input.GetButton("Player_SimulateBreathingMobile") && Input.GetAxisRaw("Player_SimulateBreathingMobile") < 0)
            {
				DeviceManager.Instance.FlowLMin = -DeviceManager.Instance.MaxFlowLMin;
            }
            else
            {
				DeviceManager.Instance.FlowLMin = 0;
            }
#endif
    }
}
