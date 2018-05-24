using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blow : MonoBehaviour {

    public Image blowIcon;

    private void Start()
    {
        blowIcon.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.transform.name);
        if (other.gameObject.CompareTag("Bestuurder"))
        {
            Debug.Log(" pieter");
            StartCoroutine(displayBlowIcon());
        }
        else
        {
            Debug.Log("nopee");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Bestuurder"))
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
            Debug.Log("on");
            yield return new WaitForSecondsRealtime(2f);
            blowIcon.enabled = false;
            Debug.Log("off");
            yield return new WaitForSecondsRealtime(2f);
        }

    }
}
