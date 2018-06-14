using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float viewRadius;    

    public LayerMask coinMask;
    private float smallestDist = 99999;
    public GameObject Magnet;
    public Transform MagnetTrans;
    [SerializeField]
    private GameObject testCoin = null;
    LineRenderer lineRenderer = new LineRenderer();
    private Transform coinpos;
    private Collider2D[] coins;
    public GameObject foundCoin;


    public float foundDist;
    public float coinDist;

    void Start()
    {

    }

    void FixedUpdate()
    {
        Vector3 forward = Magnet.transform.TransformDirection(Vector3.forward) * 10;
        if (foundCoin != null)
        {
            foundCoin.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);  
            foundCoin.transform.localScale = new Vector3(1, 1, 0.24f);
        }
        foundCoin = FindNearestCoin(coins);
    }
    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal) {
            angleInDegrees += transform.eulerAngles.y;    
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), 0);
    }

    public GameObject FindNearestCoin(Collider2D[] coins)
    {
        //Vector2 myPos = new Vector2(transform.position.x, transform.position.y);
        Collider2D[] hitCoins = Physics2D.OverlapCircleAll(this.transform.position, viewRadius, coinMask);
        GameObject tmin = null;  
        float minDist = Mathf.Infinity;
        Vector3 currentpos = transform.position;
        Debug.Log(hitCoins.Length);
        foreach(Collider2D t in hitCoins)
        {

            float dist = Vector3.Distance(t.gameObject.transform.position ,currentpos);
            if (dist < minDist)
            {
                tmin = t.gameObject;
                minDist = dist;
            }
            
        }
        return tmin;
    }

}
