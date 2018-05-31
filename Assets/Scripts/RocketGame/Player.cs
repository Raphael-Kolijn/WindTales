using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float Coins;
    Collider2D Collider;

	void Start ()
    {
        Collider = GetComponent<Collider2D>();
	}
	
	void Update ()
    {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter2D");
        AddCoin(other.gameObject);   
    }

    void AddCoin(GameObject coin)
    {
        Coins++;
        Destroy(coin);
    }
}
