using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockManager : MonoBehaviour
{
    public Image image;
    public float time = 60;
    float updatedTime;
    public PlayerController2DRocket rocket;
    public CoinSpawner coinSpawner;

    void Awake()
    {
        updatedTime = time;
    }

	void Update ()
    {
        updatedTime -= Time.deltaTime;

        image.fillAmount = updatedTime / 60;

        if (updatedTime <= 0)
        {
            rocket.m_MaxSpeed = 0;
            coinSpawner.MaySpawn = false;
        }
	}
}
