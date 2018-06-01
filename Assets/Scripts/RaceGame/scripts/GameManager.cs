using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public Text countDown;
    public GameObject gameOverUI;
    public bool RaceEnded;
    private int collectedMoney;
    float time;
    public Text RaceTime;
    public Text Money;
    public GameObject car;
  
	// Use this for initialization
	void Start () {
        RaceEnded = false;
        StartCoroutine(Countdown(3));
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
		if (Input.GetKeyDown("e"))
        {
            EndGame();
        }
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

    public void EndGame()
    {
        RaceEnded = true;
        gameOverUI.SetActive(true);
        CalculateMoney();
        RaceTime.text = time.ToString();
        Money.text = collectedMoney.ToString();
    }

    private void CalculateMoney()
    {
        collectedMoney = 300;
    }
}
