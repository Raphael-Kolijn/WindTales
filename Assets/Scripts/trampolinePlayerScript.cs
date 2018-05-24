using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class trampolinePlayerScript : MonoBehaviour {

    public Text playerHeight;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        playerHeight.text = "Height: " + Mathf.RoundToInt(transform.position.y);
	}
}
