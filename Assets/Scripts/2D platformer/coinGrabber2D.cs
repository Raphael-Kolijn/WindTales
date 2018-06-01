using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinGrabber2D : MonoBehaviour {

    

    private const string Cointotal = "CoinTotal";
    [SerializeField]
    private float _mCoinTotal;

    private void Awake()
    {
        _mCoinTotal = PlayerPrefs.GetInt(Cointotal, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Coin"))
        {
            collision.gameObject.SetActive(false);
            addCoin(collision.GetComponent<coin2D>().coinValue);
        }
    }

    private void addCoin(float coinValue)
    {
        if (coinValue < 0)
        {
            Debug.LogWarning("Cannot add a negative value");
            return;
        }

        _mCoinTotal += coinValue;
        PlayerPrefs.SetInt(Cointotal, Mathf.FloorToInt(_mCoinTotal));
    }
}
