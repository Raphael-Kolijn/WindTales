using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpScript : MonoBehaviour {

    // Controller input
    [Header("gameplay options")]
    [SerializeField]
    private DeviceManager.DeviceType deviceType;
    [SerializeField]
    [Tooltip("The amount which the player needs to blow in order to jump")]
    private double blowthreshold;

    // If the player is in the ex- or inhale phase
    private bool inhalePhase;
    // If the game has started
    private bool started;
    // Move the spring up when the exhale phase starts
    private bool exhalePhaseStarted;
    // The air being ex- or inhaled
    double flowRate;
    // The spring that launches the player
    public GameObject spring;
    // The startposition of the spring
    private float springStartPos;
    // The speed with which the spring goes down
    public float springSpeedDown;
    // '' up
    public float springSpeedUp;
    // The target for the spring to move towards
    public Transform target;
    // The target that the spring moves towards when charging
    public Transform targetForGoingDown;
    
    

    void Start () {
        // Setting up the controller
        DeviceManager.Instance.SetDeviceType(deviceType);
        started = false;
        inhalePhase = false;
        exhalePhaseStarted = false;
        playIntro();
        springStartPos = spring.transform.position.y;
        //Physics.gravity = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update () {
        getFlowrate();
        // Only triggers after the intro
        inhalePhaseLogic();
        // Only triggers when the spring is down
        moveSpringUp();
    }

    // Gets the airflow value every frame
    private void getFlowrate()
    {
        flowRate = DeviceManager.Instance.FlowLMin;
        flowRate = System.Math.Round(flowRate, 1);
        if (flowRate > 50 || flowRate < -50)
        {
            Debug.Log(flowRate.ToString());
        }
        if(flowRate < -50)
        {
            inhalePhase = true;
        }
    }

    // When the game starts countdown and give instructions
    private void playIntro()
    {
        // Intro
        started = true;
    }

    private void inhalePhaseLogic()
    {
        if(inhalePhase)
        {
            Debug.Log("Inhale phase begun");
            if (flowRate < -50 && spring.transform.position.y > springStartPos - 6)
            {
                //float amountToLower = (float)flowRate;
                //Vector3 newSpringPosition = new Vector3(0, amountToLower);
                //spring.transform.position += newSpringPosition;
                //float step = springSpeedDown * Time.deltaTime;
                //Vector3 springMaxPos = new Vector3(spring.transform.position.x, spring.transform.position.y - 6f, 0);
                //spring.transform.position = Vector3.MoveTowards(spring.transform.position, springMaxPos, step);
                float step = springSpeedDown * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, targetForGoingDown.position, step);
            }
            if(flowRate > -50)
            {
                // When the player stops inhaling the inhale phase ends and the exhale phase begins
                inhalePhase = false;
                startExhalePhase();
            }
        }
    }

    // 
    private void startExhalePhase()
    {
        Debug.Log("Exhale phase begun");
        exhalePhaseStarted = true;  
    }

    private void moveSpringUp()
    {
        if (exhalePhaseStarted && spring.transform.position.y < springStartPos)
        {
            //float step = springSpeedUp * Time.deltaTime;
            //Vector3 springMaxPos = new Vector3(spring.transform.position.x, spring.transform.position.y);
            //spring.transform.position = Vector3.MoveTowards(spring.transform.position, springMaxPos, step);
            float step = springSpeedUp * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        }
    }
}