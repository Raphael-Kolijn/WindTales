using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

    

public class MagnetOrbit : MonoBehaviour
{
    [Header("gameplay options")]
    [SerializeField]
    private DeviceManager.DeviceType deviceType;

    public double flowRate;

    [SerializeField]
    [Tooltip("The amount which the player needs to inhale in order to attract coins")]
    private double inhaleThreshold;

    bool isPressedLeft = false;
    bool isPressedRight = false;
    float rotateSpeed = 100;
    Transform test;

    void Awake()
    {
        DeviceManager.Instance.SetDeviceType(deviceType);
    }
    void Start()
    {
        test = transform.parent;
        //target = GameObject.Find("target");
    }

    void Update()
    {
        flowRate = DeviceManager.Instance.FlowLMin;

        flowRate = System.Math.Round(flowRate, 1)*-1; 
    }

    public void Rotate(int whichWay)
    {
        transform.RotateAround(test.position, new Vector3(0, 0, whichWay), rotateSpeed * Time.deltaTime); // (1 is left) (-1 is right)
       // transform.LookAt(target.transform.position); //Gives nullreferenceexception but still works?
    }
}
