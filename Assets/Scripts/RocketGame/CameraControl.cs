using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
    public Transform Rocket;
    public Transform emptyX;
    public Transform emptyY;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 newposition = transform.position;
        newposition.z = Rocket.position.z;
        newposition.z = Mathf.Clamp(Rocket.z, emptyX.position.z, emptyY.position.z);
        transform.position = newPosition;

    }
}
