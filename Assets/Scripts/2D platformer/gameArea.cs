using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameArea : MonoBehaviour
{

    public Vector3 size = new Vector3(30, 5);

    private void OnDrawGizmos()
    {
        Vector3 offset = new Vector3(size.x / 2, size.y / 2);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + offset, transform.position + size);
    }
}
