using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class death : MonoBehaviour {

    Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(other.name);
        if (other.name == "DeathZone")
        {
            Debug.Log("DIE!");
            transform.position = startPos;
        }
    }
}
