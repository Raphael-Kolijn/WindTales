using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrampCoin : MonoBehaviour
{
    public CoinScript coinManager;
    public Text coins;
    public AudioManager am;

    void OnTriggerEnter(Collider other)
    {
        coinManager.AddCoins(1);   
        int coinAmount = coinManager.GetCoinTotal();
        coins.text = coinAmount.ToString();
        am.playPickupSound();
        Destroy(this.gameObject);
    }
}
