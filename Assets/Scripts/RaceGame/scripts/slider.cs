using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class slider : MonoBehaviour {

    public Slider mainSlider;
    public ReadBlowInput input;

    public void FixedUpdate()
    {
        mainSlider.value = (int)input.getFlow();
    }

    private void Start()
    {
        mainSlider.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("NAME ==" + other.name);
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log( "nu slidaerre back");
            mainSlider.gameObject.SetActive(true);
            StartCoroutine(DisplaySlider());
        }
    }

    IEnumerator DisplaySlider()
    {
        bool active = true;
        while (active)
        {

            yield return new WaitForSecondsRealtime(3f);
            mainSlider.gameObject.SetActive(false);
            active = false;
        }

    }
}
