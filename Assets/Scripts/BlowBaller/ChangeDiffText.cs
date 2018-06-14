using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeDiffText : MonoBehaviour
{
    [SerializeField]
    private Text text;
    [SerializeField]
    private Slider slider;
	// Use this for initialization
	void Start ()
    {
        changeDifficulty();
	}

    public void changeDifficulty()
    {
        GameMaster.instance.setDifficulty(slider.value);
        text.text = "Difficulty: " + slider.value;
    }

}
