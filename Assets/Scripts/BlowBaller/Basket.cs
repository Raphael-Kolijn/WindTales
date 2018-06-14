using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
public class Basket : MonoBehaviour
{
    [SerializeField]
    private CoinScript coinManager;
    [SerializeField]
    private ParticleSystem coinsystem;
    private bool goRight = true;
    private bool goLeft = false;
    private bool scored = false;


    void OnCollisionEnter()
    {
        GameMaster.instance.scorePlusPlus();
        coinManager.AddCoins(1);
        coinsystem.Play();
        GetComponent<AudioSource>().Play();
        scored = true;
    }

    void Update()
    {
        if (GameMaster.instance != null)
        {
            if (GameMaster.instance.getMovingBasket())
            {
                if (transform.position.x >= 158)
                {
                    goRight = false;
                    goLeft = true;
                }
                else if (transform.position.x <= -84)
                {
                    goLeft = false;
                    goRight = true;
                }
                if (goRight) transform.Translate(-Vector3.right * GameMaster.instance.getBasketSpeed() * Time.deltaTime);
                else if (goLeft) transform.Translate(-Vector3.left * GameMaster.instance.getBasketSpeed() * Time.deltaTime);
            }
            else if (transform.position != GameMaster.instance.getBasketStartPosition())
            {
                Reset();
            }
        }
    }

    public void Reset()
    {
        if (scored)
        {
            GameMaster.instance.setBasketSpeed();
            scored = false;
        }
        coinsystem.Stop();
        transform.position = GameMaster.instance.getBasketStartPosition();
        GameMaster.instance.setMinimumValue();
    }
}