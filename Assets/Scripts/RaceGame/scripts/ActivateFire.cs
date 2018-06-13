using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateFire : MonoBehaviour {


    public GameObject rightBoost;
    public GameObject leftBoost;
    public GameObject psLeft;
    public GameObject psRight;
    private ParticleSystem particleLeft;
    private ParticleSystem particleRight;

    private void Start()
    {

        
        rightBoost.GetComponent<MeshRenderer>().enabled = false;
        leftBoost.GetComponent<MeshRenderer>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("HEH?");
   
        particleLeft = psLeft.GetComponent<ParticleSystem>();
        particleRight = psRight.GetComponent<ParticleSystem>();
        if (other.gameObject.CompareTag("Speed up"))
        {
            particleLeft.Play();
            particleRight.Play();
            Debug.Log("boost activated");
            StartCoroutine(ActivateBoost());

        }
     }

    IEnumerator ActivateBoost()
    {
 
        bool paniek = true;
        while (paniek)
        {
            rightBoost.GetComponent<MeshRenderer>().enabled = true;
            leftBoost.GetComponent<MeshRenderer>().enabled = true;
            Debug.Log(rightBoost.GetComponent<MeshRenderer>().enabled);
            yield return new WaitForSecondsRealtime(5f);

            rightBoost.GetComponent<MeshRenderer>().enabled = false;
            leftBoost.GetComponent<MeshRenderer>().enabled = false;
            particleLeft.Stop();
            particleRight.Stop();
            paniek = false;

        }


    }
}
