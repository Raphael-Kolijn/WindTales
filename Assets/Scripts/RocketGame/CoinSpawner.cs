using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject Coin;
    public GameObject Rocket;
    public int CoinSpawnRate = 150;
    public bool MaySpawn = true;

    List<GameObject> coins = new List<GameObject>();
    GameObject newCoin;
    Vector3 newPosition;


    void Start()
    {
        newPosition.z -= 1f;
        UpdateCoins();
    }

	void Update ()
    {
        if (Random.Range(0, CoinSpawnRate) == 1 && MaySpawn)
        {
            SpawnCoin();
            DeleteCoins();
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
        newPosition.x = Random.Range(-24f, 24f);
        newPosition.y = Rocket.transform.position.y + 20f;
        newCoin = Instantiate(Coin);
        newCoin.transform.position = newPosition;
        newCoin.layer = 9;

        coins.Add(newCoin);
    }

    void DeleteCoins()
    {
        List<GameObject> pastCoins = new List<GameObject>();
        foreach (GameObject foundCoin in coins)
        {
            if (foundCoin != Coin && foundCoin != null)
            {
                if (foundCoin.transform.position.y + 20f < Rocket.transform.position.y)
                {
                    pastCoins.Add(foundCoin);
                }
            }
        }
        foreach (GameObject pastCoin in pastCoins)
        {
            if (coins.Contains(pastCoin))
            {
                if (pastCoin != null)
                {
                    coins.Remove(pastCoin);
                    Destroy(pastCoin);
                }
            }   
        }
    }
}
