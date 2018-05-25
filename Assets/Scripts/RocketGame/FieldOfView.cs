using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float viewRadius;

    public LayerMask coinMask;
    private float smallestDist = 99999;
    public GameObject Magnet;
    [SerializeField]
    private GameObject testCoin = null;

    public float foundDist;
    public float coinDist;

  



    void FixedUpdate()
    {
        Vector3 forward = Magnet.transform.TransformDirection(Vector3.forward) * 10;
        FindNearestCoin();
        Magnet.transform.LookAt(testCoin.transform.position);
        Debug.DrawRay(Magnet.transform.position, testCoin.transform.position*100 , Color.red);
    }
    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal) {
            angleInDegrees += transform.eulerAngles.y;    
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), 0);
    }

    public void FindNearestCoin()
    {
        Vector2 myPos = new Vector2(transform.position.x, transform.position.y);
        Collider2D[] hitCoins = Physics2D.OverlapCircleAll(myPos, viewRadius, coinMask);
        Debug.Log("Found: " + hitCoins.Length + " coins");
        for (int i = 0; i <= hitCoins.Length - 1; i++)
        {
            if (testCoin == null)
            {
                testCoin = hitCoins[i].gameObject;
                Debug.Log("null");
            }
             foundDist = Vector3.Distance(transform.position, hitCoins[i].transform.position);
             coinDist = Vector3.Distance(transform.position , testCoin.transform.position);
        

            if (foundDist < coinDist)
            {
                Debug.Log("object vervangen");
                testCoin = hitCoins[i].gameObject;
                return;
            }    
        }
        Debug.Log("niets gevonden");
    }
}
