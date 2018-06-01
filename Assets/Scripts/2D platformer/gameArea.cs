﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameArea : MonoBehaviour
{

    public Vector3 size = new Vector3(15, 5);

    private void OnDrawGizmos()
    {
        Vector3 offset = new Vector3(size.x / 2, size.y / 2);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + offset, size);
    }

    public void OnEnable()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject temp = transform.GetChild(i).gameObject;
            if (!temp.activeSelf)
            {
                temp.SetActive(true);
            }
        }
    }
}
