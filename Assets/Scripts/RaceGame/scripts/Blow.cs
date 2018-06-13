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
        Debug.Log("hit something");
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(displayBlowIcon());
        }
    }

    IEnumerator displayBlowIcon()
    {
        bool paniek = true;
        while (paniek)
        {
            
            blowIcon.enabled = true;
            yield return new WaitForSecondsRealtime(3f);
            blowIcon.enabled = false;
            paniek = false;
        }

    }
}
