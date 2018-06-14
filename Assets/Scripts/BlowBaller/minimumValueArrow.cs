using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class minimumValueArrow : MonoBehaviour
{
    private Vector3 startPosition;


    void Start()
    {
        startPosition = transform.localPosition;
    }
    // Use this for initialization
    public void changePosition(Slider slider, int minimumValue)
    {
        RectTransform rt = (RectTransform)slider.transform;
        float i = rt.rect.width / (slider.maxValue + 1);
        if ((startPosition.x + ( i* minimumValue)) - startPosition.x <= 250)
        {
            transform.localPosition = new Vector3(startPosition.x + (i * minimumValue) - 10, transform.localPosition.y, transform.localPosition.z);
        }

    }
}
