﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSuction : MonoBehaviour
{
    public MagnetOrbit magnetOrbit;
    public GameObject coin;
    float speed = 2f;

	void Update ()
    {
        Move();
	}

    void Move()
    {
        float step = speed * Time.deltaTime;
        coin.transform.position = Vector3.MoveTowards(coin.transform.position, transform.position, step);
        speed = (speed * (1.9f * 1.9f) * (0.3f)) + 0.1f;
    }
}