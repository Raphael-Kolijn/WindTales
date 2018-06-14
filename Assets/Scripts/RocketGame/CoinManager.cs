using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public float coins;
    public Text text;

    public void AddCoin(GameObject coin)
    {
        coins++;
        Destroy(coin);

        text.text = coins.ToString();
    }
}
