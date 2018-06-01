using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject Coin;
    public GameObject Rocket;

    List<GameObject> coins = new List<GameObject>();
    GameObject newCoin;
    Vector3 newPosition;
    bool MaySpawn = true;

    void Start()
    {
        newPosition.z -= 0.5f;
        UpdateCoins();
    }

	void Update ()
    {
        if (Random.Range(0, 100) == 1)
        {
            DeleteCoins();
            MaySpawn = true;
        }

        if (MaySpawn)
        {
            SpawnCoin();
            MaySpawn = false;
        }
    }

    void UpdateCoins()
    {
        GameObject[] findCoins = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[];
        foreach (GameObject foundCoin in findCoins)
        {
            if (foundCoin.layer == 9)
            {
                coins.Add(foundCoin);
            }
        }
    }

    void SpawnCoin()
    {
        newPosition.x = Random.Range(-28f, 28f);
        newPosition.y = Rocket.transform.position.y + 20f;
        newCoin = Instantiate(Coin);
        newCoin.transform.position = newPosition;

        coins.Add(newCoin);
    }

    void DeleteCoins()
    {
        List<GameObject> pastCoins = new List<GameObject>();
        foreach (GameObject foundCoin in coins)
        {
            if (foundCoin.transform.position.y + 20f < Rocket.transform.position.y)
            {
                if (foundCoin != Coin && foundCoin != null)
                {
                    pastCoins.Add(foundCoin);
                }
            }
        }
        foreach (GameObject pastCoin in pastCoins)
        {
            if (coins.Contains(pastCoin))
            {
                coins.Remove(pastCoin);
                Destroy(pastCoin);
            }
        }
    }
}
