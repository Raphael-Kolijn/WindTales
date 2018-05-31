using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public Text countDown;
  
	// Use this for initialization
	void Start () {
        StartCoroutine(Countdown(3));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Countdown(int seconds)
    {
        int count = seconds;

        while (count > 0)
        {
            countDown.text = count.ToString();
            yield return new WaitForSeconds(1);
            count--;
        }

        StartGame();
    }

    void StartGame()
    {
        countDown.text = "";
        Debug.Log("start game");

    }
}
