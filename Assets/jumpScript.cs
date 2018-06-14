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
    private float launchSpeed;
    // The amount the player is boosted
    public float thrust;
    // The text to fill in at the end of the game
    public Text endText;
    // The text to show current flowrate
    public Text flowRateText;
    // Text to show the amount being boosted
    public Text boostText;
    // Text to show the launch strength
    public Text launchText;
    // Audiomanager for sound
    public AudioManager am;
    // The gamemanager controls the UI among other things
    public gameManagerScript gm;
    [Header("Difficulty - max 10")]
    public int difficulty;


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
        Debug.Log(springStartPos.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        // Stop getting the flowrate when the game ends
        if (ended == false)
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
        flowRate = System.Math.Round(flowRate, 1) * -1;
        if (flowRate > 10 || flowRate < -10)
        {
            // Debug.Log(flowRate.ToString());
        }
        if (flowRate < -10)
        {
            inhalePhase = true;
        }
        if (exhalePhaseStarted)
        {
            // While the exhalation continues force is added to the player
            endText.text = ("Exhale!");
            Vector3 forceToAdd = transform.up * ((float)flowRate / 4);
            player.AddForce(forceToAdd);
            boostText.text = "Force: " + System.Math.Round(forceToAdd.y).ToString();
            StartCoroutine(checkForEndPhase());
        }
    }

    IEnumerator checkForEndPhase()
    {
        while (true)
        {
            yield return new WaitForSeconds(2.0f);
            if (flowRate < 5)
            {
                break;
            }
        }
        endGame();
    }

    // End the game when the exhalationphase ends 
    private void endGame()
    {
        exhalePhaseStarted = false;
        inhalePhase = false;
        started = false;
        endText.text = "Finished!";
        am.stopWind();
        ended = true;
        player.velocity.Set(0, 0, 0);
        player.AddTorque(player.position);
        gm.enableButtons();
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
            // Debug.Log("Inhale phase begun");
            if (flowRate < -10 && spring.transform.position.y > targetForGoingDown.position.y)
            {
                float step = springSpeedDown * Time.deltaTime;
                am.playSpringTick();
                transform.position = Vector3.MoveTowards(transform.position, targetForGoingDown.position, step);
            }
            if (flowRate > -5 && exhalePhaseStarted == false)
            {
                // When the player stops inhaling the inhale phase ends and the exhale phase begins
                launchSpeed = springStartPos - transform.position.y * 10 + difficulty;
                launchText.text = "Launch speed: " + launchSpeed.ToString();
                inhalePhase = false;
                startExhalePhase();
            }
        }
    }

    // After the player launches force can be added by continuous exhalation
    private void startExhalePhase()
    {
        // Debug.Log("Exhale phase begun");
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
        am.playLaunchSound();
        am.startWind();
        player.AddForce(transform.up * launchSpeed / 2, ForceMode.Impulse);
    }
}