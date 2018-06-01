using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameManagerScript : MonoBehaviour
{

    public Button endGameBtn;
    public Button restartBtn;

    // Use this for initialization
    void Start()
    {
        endGameBtn.interactable = false;
        restartBtn.interactable = false;
    }

    public void enableButtons()
    {
        endGameBtn.interactable = true;
        restartBtn.interactable = true;
    }
}
