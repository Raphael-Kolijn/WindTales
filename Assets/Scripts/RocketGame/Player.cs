using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float Coins;
    BoxCollider2D boxCollider;

	void Start ()
    {
        boxCollider = GetComponent<BoxCollider2D>();
	}
	
	void Update ()
    {
		
	}

    void OnTriggerEnter2D(GameObject other)
    {
        Debug.Log("OnTriggerEnter2D");
        AddCoin(other);   
    }

    void AddCoin(GameObject coin)
    {
        Coins++;
        Destroy(coin);
    }
}
