using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;

    public LayerMask coinMask;
    private float smallestDist = Mathf.Infinity;

    void Update()
    {
        transform.LookAt(FindNearestCoin().transform);
    }
    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal) {
            angleInDegrees += transform.eulerAngles.y;    
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), 0);
    }

    public GameObject FindNearestCoin()
    {
        Collider[] hitCoins = Physics.OverlapSphere(transform.position, viewRadius, coinMask);
        GameObject possibleNearest = null;
        for (int i = 0; i < hitCoins.Length; i++)
        {
            float foundDist = Vector3.Distance(transform.position, hitCoins[i].transform.position);
            if (foundDist < smallestDist)
            {
                smallestDist = foundDist;
                possibleNearest = hitCoins[i].gameObject;
            }
        }
        if (possibleNearest != null)
        {
            Debug.Log("NOT NULL");
        }
        return possibleNearest;
    }
}
