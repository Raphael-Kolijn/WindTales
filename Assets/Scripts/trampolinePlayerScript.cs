using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class trampolinePlayerScript : MonoBehaviour
{

    public Text playerHeight;
    private float currentHeight;
    private float previousHeight;
    public Text scoreText;
    private int score;
    private int HighScore;
    public Text highScoreText;
    private Rigidbody rb;
    private bool ended;
    public AudioManager am;


    // Use this for initialization
    void Start()
    {
        currentHeight = transform.position.y;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        previousHeight = currentHeight;
        currentHeight = transform.position.y;
        playerHeight.text = "Height: " + Mathf.RoundToInt(transform.position.y);
        if (transform.position.y > 5)
        {
            if (currentHeight < previousHeight)
            {
                ended = true;
                endGame();
            }
        }
    }

    void endGame()
    {
        if (ended)
        {
            rb.velocity = Vector3.zero;
            am.stopMusic();
            am.playEndSound();
            scoreText.text = "Your score: " + System.Math.Round(transform.position.y).ToString();
            score = (int)System.Math.Round(transform.position.y);
            if (score > HighScore)
            {
                HighScore = score;
            }
            ended = false;
        }
    }
}
