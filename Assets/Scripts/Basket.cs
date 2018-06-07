using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
public class Basket : MonoBehaviour
{
    public ParticleSystem coinsystem;
    private bool goRight = true;
    private bool goLeft = false;
    private int currentScore;


    void OnCollisionEnter()
    {
        currentScore++;
        coinsystem.Play();
        GetComponent<AudioSource>().Play();
    }

    void Update()
    {
        if (transform.position.x >= 200)
        {
            goRight = false;
            goLeft = true;
        }
        else if (transform.position.x <= -76)
        {
            goLeft = false;
            goRight = true;
        }
        if (goRight) transform.Translate(-Vector3.right * GameMaster.instance.getBasketSpeed() * Time.deltaTime);
        else if (goLeft) transform.Translate(-Vector3.left * GameMaster.instance.getBasketSpeed() * Time.deltaTime);
    }

    public void Reset()
    {
        coinsystem.Stop();
        transform.position = GameMaster.instance.getBasketStartPosition();
        GameMaster.instance.setMinimumValue(currentScore);
    }
}