using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtObject : MonoBehaviour
{
    public Transform Target;
    public float RotateSpeed;   

    
	// Update is called once per frame
	void Update ()
    {
        var direction = Target.transform.position - transform.position;

        // Set Y the same to make the rotations turret-like:
        direction.y = transform.position.y;

        var rot = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(
                                         transform.rotation,
                                         rot,
                                         RotateSpeed * Time.deltaTime);   

    }
}
