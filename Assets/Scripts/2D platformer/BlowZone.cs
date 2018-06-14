using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]
public class BlowZone : MonoBehaviour {

    public Image blowIcon;
    private BoxCollider2D col;
    static PlayerController2D player;

    public float speed;

    bool playerHasEnteredBlowZone = false;

    private void Start()
    {
        if (blowIcon == null)
        {
             blowIcon = GameObject.Find("blowIcon").GetComponent<Image>();
        }

        blowIcon.enabled = false;
    }

    //private void FixedUpdate()
    //{
    //    if (playerHasEnteredBlowZone)
    //    {
    //        Vector2 target = new Vector2(transform.position.x, player.transform.position.y);
    //        if (Vector2.Distance(player.transform.position, target)<0.05f)
    //        {
    //            player.m_Anim.SetFloat("Speed", Mathf.Abs(speed));
    //        }
    //        else
    //        {
    //            Vector2 velocity = player.m_Rigidbody2D.velocity;
    //            player.transform.position = Vector2.SmoothDamp(player.transform.position, target, ref velocity, speed, player.m_MaxSpeed, Time.fixedDeltaTime);
    //        }
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.transform.name);
        if (collision.transform.CompareTag("Player"))
        {
            //if (!player)
            //{
            //    if (player = collision.gameObject.GetComponent<PlayerController2D>())
            //    {
            //        playerHasEnteredBlowZone = true;
            //        player.enabled = false;
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
