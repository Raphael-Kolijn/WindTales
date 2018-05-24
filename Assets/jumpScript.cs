using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class jumpScript : MonoBehaviour
{

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
    // When the game ends
    private bool ended;
    // The air being ex- or inhaled
    double flowRate;
    // The spring that launches the player
    public GameObject spring;
    // Get the player object rigidbody to control it during the exhale phase
    public Rigidbody player;
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
    // The thrust with which the player is launched. 80 = max
    [Header("max 80")]
    public float launchSpeed;
    // The amount the player is boosted
    public float thrust;
    // The text to fill in at the end of the game
    public Text endText;
    // The text to show current flowrate
    public Text flowRateText;
    // Text to show the amount being boosted
    public Text boostText; 

    void Start()
    {
        // Setting up the controller
        DeviceManager.Instance.SetDeviceType(deviceType);
        started = false;
        inhalePhase = false;
        exhalePhaseStarted = false;
        ended = false;
        playIntro();
        springStartPos = spring.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        // Stop getting the flowrate when the game ends
        if(ended == false)
        {
            getFlowrate();
        }
        // Only triggers after the intro
        inhalePhaseLogic();
        // Only triggers when the spring is down
        moveSpringUp();
        flowRateText.text = ("Flowrate: " + (Mathf.RoundToInt((float)flowRate)));
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
        if (flowRate < -50)
        {
            inhalePhase = true;
        }
        if (exhalePhaseStarted)
        {
            // While the exhalation continues force is added to the player
            endText.text = ("Exhale!");
            Vector3 forceToAdd = transform.up * (float)flowRate / 3;
            player.AddForce(forceToAdd);
            boostText.text = "Force: " + forceToAdd.y.ToString();
            StartCoroutine(checkForEndPhase());
        }
    }

    IEnumerator checkForEndPhase()
    {
        while (true)
        {
            yield return new WaitForSeconds(2.0f);
            if(flowRate <5)
            {
                break;
            }
        }
        endGame();
    }

    // End the game when the exhalationphase ends 
    private void endGame()
    {
        Debug.Log("The game has ended");
        exhalePhaseStarted = false;
        inhalePhase = false;
        started = false;
        endText.text = "Finished!";
        ended = true;
        // SceneManager.LoadScene("TrampolineGuy");
    }

    // When the game starts countdown and give instructions
    private void playIntro()
    {
        // Intro
        started = true;
    }

    // Moves the spring down 
    private void inhalePhaseLogic()
    {
        if (inhalePhase)
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
            if (flowRate > -5 && exhalePhaseStarted == false)
            {
                // When the player stops inhaling the inhale phase ends and the exhale phase begins
                inhalePhase = false;
                startExhalePhase();
            }
        }
    }

    // After the player launches force can be added by continuous exhalation
    private void startExhalePhase()
    {
        Debug.Log("Exhale phase begun");
        launchPlayer();
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

    // The initial launch
    private void launchPlayer()
    {
        // TODO: base the added force on the spring position
        player.AddForce(transform.up * launchSpeed, ForceMode.Impulse);
    }
}