using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
public class Basket : MonoBehaviour
{
    public GameObject score;
    private bool goRight = true;
    private bool goLeft = false;
    private int currentScore;


    void OnCollisionEnter()
    {
        // als je de basket raakt
    }

    void Update()
    {
        if (GameMaster.instance.getMovingBasket() == true)
        {
            if (transform.position.x >= 208)
            {
                goRight = false;
                goLeft = true;
            }
            else if (transform.position.x <= -71)
            {
                goLeft = false;
                goRight = true;
            }
            if (goRight) transform.Translate(-Vector3.right * GameMaster.instance.getBasketSpeed() * Time.deltaTime);
            else if (goLeft) transform.Translate(-Vector3.left * GameMaster.instance.getBasketSpeed() * Time.deltaTime);
        }
    }

    void OnTriggerEnter()
    {
        currentScore++;
        score.GetComponent<Text>().text = currentScore.ToString();
        GetComponent<AudioSource>().Play();
    }

    public void Reset()
    {
        transform.position = GameMaster.instance.getBasketStartPosition();
        GameMaster.instance.setMinimumValue(currentScore);
    }
}