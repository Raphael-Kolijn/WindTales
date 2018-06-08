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


    void OnCollisionEnter()
    {
        GameMaster.instance.scorePlusPlus();
        coinManager.AddCoins(1);
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
        GameMaster.instance.setBasketSpeed();
        coinsystem.Stop();
        transform.position = GameMaster.instance.getBasketStartPosition();
        GameMaster.instance.setMinimumValue();
    }
}