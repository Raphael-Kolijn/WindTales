using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtObject : MonoBehaviour
{
    public GameObject Target;
    public float RotateSpeed;

    // Update is called once per frame
    void Update ()
    {
        Target = GetComponent<FieldOfView>().foundCoin;
        if (Target != null)
        {
            transform.LookAt(Target.transform, Vector3.forward);
            transform.Rotate(0, 90, 0);
        }
        /*
        var direction = Target.transform.position - transform.position;

        // Set Y the same to make the rotations turret-like:
        direction.z = transform.position.z;

        
        var rot = Quaternion.LookRotation(direction, Vector3.up);
        
        transform.rotation = Quaternion.RotateTowards(
                                         transform.rotation,
                                         rot,
                                         RotateSpeed * Time.deltaTime);
        */
        //transform.Rotate(0, 100 * Time.deltaTime, 0);
    }
}
