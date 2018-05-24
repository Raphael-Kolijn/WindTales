using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coin;

    GameObject newCoin;
    Vector3 newPosition;
    Quaternion newRotation;
    bool MaySpawn = false;

	void Update ()
    {
        if (!MaySpawn)
        {
            newPosition = new Vector3(newCoin.transform.position.x, newCoin.transform.position.y + 10f, newCoin.transform.position.z);
            newRotation = new Quaternion();
            MaySpawn = !MaySpawn;

            newCoin = Instantiate(coin, newPosition, newRotation);
        }
    }
}
