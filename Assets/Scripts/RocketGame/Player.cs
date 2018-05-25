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

<<<<<<< HEAD
    void OnTriggerEnter2D(BoxCollider2D other)
=======
    void OnTriggerEnter2D(Collider other)
>>>>>>> 3b5801e347308a491f7d31f4ee6e38131f9aa947
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
