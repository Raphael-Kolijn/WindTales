using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Waypoint : MonoBehaviour {
    BoxCollider2D col;

	// Use this for initialization
	void Start () {
        if (!col)
        {
            col = GetComponent<BoxCollider2D>();
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnDrawGizmos()
    {
        if (!col)
        {
            col = GetComponent<BoxCollider2D>();
        }
        if (col.offset.x != col.size.x / 2)
        {
            col.offset = new Vector2(col.size.x / 2, col.offset.y);
        }
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(new Vector3(col.offset.x, col.offset.y) + transform.position, new Vector3(col.size.x, col.size.y));
    }
}
