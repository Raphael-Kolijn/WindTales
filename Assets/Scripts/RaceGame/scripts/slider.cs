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
}
