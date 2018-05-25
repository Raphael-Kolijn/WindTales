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

    void OnTriggerEnter2D(BoxCollider2D other)
    {
        Debug.Log("OnTriggerEnter2D");
        AddCoin(other.gameObject);   
    }

    void AddCoin(BoxCollider2D coin)
    {
        Coins++;
        Destroy(coin);
    }
}
