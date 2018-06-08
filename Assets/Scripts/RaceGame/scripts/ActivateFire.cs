using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateFire : MonoBehaviour {


    public GameObject rightBoost;
    public GameObject ps;
    private ParticleSystem particle;

    private void Start()
    {

   
        rightBoost.GetComponent<MeshRenderer>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
     
        //   particle = ps.GetComponent<ParticleSystem>();
        //  particle.Play();
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("boost activated");
            StartCoroutine(ActivateBoost());
        }
        Debug.Log(rightBoost.GetComponent<MeshRenderer>().enabled);
    }

    IEnumerator ActivateBoost()
    {
 
        bool paniek = true;
        while (paniek)
        {
            rightBoost.GetComponent<MeshRenderer>().enabled = true;
            Debug.Log(rightBoost.GetComponent<MeshRenderer>().enabled);
            yield return new WaitForSecondsRealtime(5f);

            //rightBoost.GetComponent<MeshRenderer>().enabled = false;
            paniek = false;
        }


    }
}
