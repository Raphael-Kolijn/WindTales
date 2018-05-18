using System;
using System.IO;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class DeviceManager : PersistentSingleton<DeviceManager>
{
    public enum DeviceType
    {
        AIRNEXT,
        CONTROLLER,
        KUEFFNER
    }

    public enum FlowState
    {
        NONE,
        SILENCE,
        FLOW_IN,
        FLOW_OUT
    }

    [SerializeField]
    [Tooltip("Type of device to connect to.")]
    private DeviceType deviceType;
    [SerializeField]
    [Tooltip("If enabled, flow values are written to a text file.")]
    private bool writeDeviceLog = false;
    [SerializeField]
    [Tooltip("Dragging a device log text file into this field will cause the device manager to use the flow values in the text file as input.")]
    private TextAsset deviceLog;

    private SpiroController spiroController; //Currently connected device (null if no device is connected).

    private double flowLMin; //Current flow value (liters per minute) registered by the spirometer.

    private float minFlowLMin = 10f; //FlowLMin values exceeding this threshold will register as a flow state change (FLOW_IN or FLOW_OUT).
    private float maxFlowLMin = 200f; //For calculations/functions requiring a maximum flow threshold (the actual FlowLMin value may still exceed this threshold).
    private float currentFlowPercentage; //Percentage of the current flow based on the above min and max values.

    private FlowState previousState; //Flowstate in previous Update
    private FlowState currentState; //The current updated flow state

    public double FlowLMin
    {
        get { return flowLMin; }
        set { flowLMin = value; }
    }

    public float MinFlowLMin
    {
        get { return minFlowLMin; }
    }

    public float MaxFlowLMin
    {
        get { return maxFlowLMin; }
    }

    public float CurrentFlowPercentage
    {
        get { return currentFlowPercentage; }
    }

    public FlowState CurrentFlowState
    {
        get
        {
            return currentState;
        }
    }

    private bool lastConnectionState = false;
    private bool currentConnectionState = false;

    public delegate void DeviceStateChanged(FlowState state);
    public DeviceStateChanged onDeviceStateChanged;

    private bool debug = false;

    void Start()
    {
        Init();
    }

    /// <summary>
    /// Initializes the SpiroController, cancels existing spirometer connection (if any) and connects to a device of the specified device type.
    /// </summary>
    private void Init()
    {
        //Debug.Log("Init device manager");
        InitMinMax();

        if (spiroController != null) //Disconnect with current device before establishing new connection
        {
            //Debug.Log("return want spiro is niet null");
            spiroController.DisconnectDevice();
            Invoke("Init", 0.5f); //Call init again after 0.5 seconds (delay to make sure device has time to disconnect)
            return;
        }
        //Debug.Log("Ik ga langs de if statements");
        if (deviceType == DeviceType.AIRNEXT)
        {
            //Debug.Log("Ik ben een airnext");
            spiroController = new BLESpiroController();
        }
        if (deviceType == DeviceType.CONTROLLER)
        {
            //Debug.Log("Ik ben een controller");
            spiroController = new SimulatedSpiroController();
        }

        if (deviceType == DeviceType.KUEFFNER)
        {
            //Debug.Log("Ik ben een spiro");
            spiroController = new USBSpiroController();
        }

        ConnectSpirometer();
    }

    void FixedUpdate()
    {
        if (spiroController == null)
        {
            return;
        }

        spiroController.UpdateSpiroController();
        currentState = spiroController.GetFlowState();

        currentFlowPercentage = ((float)FlowLMin / maxFlowLMin) * 100;

        if (debug)
        {
            Debug.Log(currentState + " " + FlowLMin.ToString("f"));
        }

        if (currentState != previousState)
        {
            if (onDeviceStateChanged != null)
            {
                onDeviceStateChanged(currentState);
            }
        }

        previousState = currentState;

        //Show/Hide disconnect screen if connection state changes
        currentConnectionState = spiroController.IsConnected;
        if (currentConnectionState != lastConnectionState)
        {
            lastConnectionState = currentConnectionState;
            if (deviceType == DeviceType.AIRNEXT)
            {
                Time.timeScale = 1; //Establishing bluetooth connection to spirometer does not work when timescale is 0!
            }
        }

    }

    public void SetDeviceType(DeviceType deviceType)
    {
        //disconnect previous device if the device type has changed
        if (deviceType != this.deviceType)
        {
            DisconnectSpirometer();
        }
        this.deviceType = deviceType;

    }

    public void ConnectSpirometer()
    {
        if (spiroController != null)
        {
            Debug.Log("Connecting device...");
            spiroController.ConnectDevice();
        }
    }

    public void InitMinMax()
    {
        string minLMin = PlayerPrefs.GetString("minLMin", "10");
        string maxLMin = PlayerPrefs.GetString("maxLMin", "200");

        minFlowLMin = (float)Decimal.Parse(minLMin);
        maxFlowLMin = (float)Decimal.Parse(maxLMin);
    }

    public void MakeSpiroControllerNull()
    {
        Invoke("SpiroControllerNull", 1);
    }

    void SpiroControllerNull()
    {
        spiroController = null;
    }

    public void DisconnectSpirometer()
    {
        if (spiroController != null)
        {
            spiroController.DisconnectDevice();
            spiroController = null;
        }
    }

    void OnApplicationPause(bool b)
    {
        if (b)
        {
            DisconnectSpirometer();
        }
    }
}
