﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSuction : MonoBehaviour
{
    public CoinManager coinManager;
    public GameObject coin;
    public float SuctionSpeed = 0.3f;

    FieldOfView fieldOfView;
    float speed = 2f;


    void Update()
    {
        fieldOfView = GetComponent<FieldOfView>();
        if (fieldOfView != null)
        {
            coin = fieldOfView.foundCoin;
        }
        if (coin != null)
        {
            Suction();
        }
        else
        {
            speed = 2f;
        }
    }

    void Suction()
    {
        if (GetComponent<MagnetOrbit>().flowRate < -50)
        {
            float step = speed * Time.deltaTime;
            coin.transform.position = Vector3.MoveTowards(coin.transform.position, transform.position, step);
            speed = (speed * (1.9f * 1.9f) * (SuctionSpeed)) + 0.1f;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == 9)
        {
            coinManager.AddCoin(coin);
        }
    }
}