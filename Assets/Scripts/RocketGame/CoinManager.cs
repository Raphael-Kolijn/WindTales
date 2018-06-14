using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    private int coins;
    public Text text;

    public void AddCoin(GameObject coin)
    {
        coins++;

        Destroy(coin);

        UpdateText();
    }

    public int GetCoins()
    {
        return coins;
    }

    private void UpdateText()
    {
        if (coins > 999)
        {
            text.text = "x" + coins.ToString();
        }
        else if (coins > 99)
        {
            text.text = "x0" + coins.ToString();
        }
        else if (coins > 9)
        {
            text.text = "x00" + coins.ToString();
        }
        else if (coins >= 0)
        {
            text.text = "x000" + coins.ToString();
        }

    }


}
