using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetOrbit : MonoBehaviour
{
    public float speed;
    public Transform target;
    

	// Use this for initialization

    void FixedUpdate()
    {
        transform.RotateAround(target.position, Vector3.forward, speed);
    }
}
