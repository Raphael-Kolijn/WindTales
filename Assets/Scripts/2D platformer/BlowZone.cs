using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]
public class BlowZone : MonoBehaviour {

    public Image blowIcon;
    private BoxCollider2D col;
    PlayerController2D player;

    private void Start()
    {
        if (blowIcon == null)
        {
             blowIcon = GameObject.Find("blowIcon").GetComponent<Image>();
        }

        blowIcon.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.transform.name);
        if (collision.transform.CompareTag("Player"))
        {
            //if (!player)
            //{
            //    if (player = collision.gameObject.GetComponent<PlayerController2D>())
            //    {
            //        player.inBlowZone = true;
            //    }
            //}
            
            StartCoroutine(displayBlowIcon());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            StopAllCoroutines();
            blowIcon.enabled = false;
        }
    }

    IEnumerator displayBlowIcon()
    {
        while (true)
        { 
            blowIcon.enabled = true;
            //Debug.Log("on");
            yield return new WaitForSecondsRealtime(0.3f);
            blowIcon.enabled = false;
            //Debug.Log("off");
            yield return new WaitForSecondsRealtime(0.3f);
        }
        
    }

    private void OnDrawGizmos()
    {
        if (col == null)
        {
            col = GetComponent<BoxCollider2D>();
        }

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + new Vector3(0,col.bounds.size.y/2 + 0.5f), col.bounds.size);
    }
}
