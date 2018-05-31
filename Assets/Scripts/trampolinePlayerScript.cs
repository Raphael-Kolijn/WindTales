using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class trampolinePlayerScript : MonoBehaviour {

    public Text playerHeight;
    // The previous height of the player to determine when they start dropping
    private float previousPlayerHeight;
    // The current height to compare
    private float currentPlayerHeight;

    // Use this for initialization
    void Start () {
        previousPlayerHeight = 0;
        currentPlayerHeight = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
        playerHeight.text = "Height: " + Mathf.RoundToInt(transform.position.y);
        previousPlayerHeight = currentPlayerHeight;
        currentPlayerHeight = transform.position.y;
        if (transform.position.y > 5)
        {
            if (previousPlayerHeight < currentPlayerHeight)
            {
                Debug.Log("Stop game");
            }
        }
	}
}