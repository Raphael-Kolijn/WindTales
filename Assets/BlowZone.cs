using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlowZone : MonoBehaviour {

    public Image blowIcon;

    private void Start()
    {
        blowIcon.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.transform.name);
        if (collision.transform.CompareTag("Player"))
        {
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
}
