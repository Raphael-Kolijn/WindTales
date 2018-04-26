using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base spirometer class
/// </summary>
public abstract class SpiroController
{

    protected bool isConnected;

	public bool IsConnected
	{
		get{return isConnected;}
	}

    public abstract void ConnectDevice();

    public abstract void UpdateSpiroController();

    public abstract void DisconnectDevice();

    public DeviceManager.FlowState GetFlowState()
    {
		if (Mathf.Abs((float)DeviceManager.Instance.FlowLMin) > DeviceManager.Instance.MinFlowLMin)
        {
			if (DeviceManager.Instance.FlowLMin > 0)
            {
                return DeviceManager.FlowState.FLOW_IN;
            }
			if (DeviceManager.Instance.FlowLMin < 0)
            {
                return DeviceManager.FlowState.FLOW_OUT;
            }
        }
        return DeviceManager.FlowState.SILENCE;
    }

	public void  SetConnected(bool b)
    {
		isConnected = b;
	}
}
