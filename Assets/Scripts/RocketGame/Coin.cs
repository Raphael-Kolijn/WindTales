using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    CoinScript coinManager;

	void Update()
    {
        transform.Rotate(new Vector3(0, 75, 00) * Time.deltaTime);
    }
}
