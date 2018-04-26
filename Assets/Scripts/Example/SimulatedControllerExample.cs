using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class SimulatedControllerExample : MonoBehaviour
{

    private Text text;

	// Use this for initialization
	void Start ()
    {
        text = GetComponent<Text>();

        DeviceManager.Instance.SetDeviceType(DeviceManager.DeviceType.CONTROLLER);
        DeviceManager.Instance.Init();
	}
	
	// Update is called once per frame
	void Update ()
    {
        text.text = "Current flow: " + DeviceManager.Instance.FlowLMin + "; Flow state: " + DeviceManager.Instance.CurrentFlowState;
	}
}
