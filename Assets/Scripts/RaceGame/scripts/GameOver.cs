using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

 //   public Text gameOver;

    private void OnEnable()
    {
      //  gameOver.text = "time placeholder";
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Racing_Game");
    }

    public void GoBackToHub()
    {
        SceneManager.LoadScene("Hub");
    }
}
