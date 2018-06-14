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

    public CoinScript coinScript;
    public AudioClip win;
    public AudioSource winMusic;


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

    }

    public void EndGame()
    {
        RaceEnded = true;
        gameOverUI.SetActive(true);
        Debug.Log("WOHOOOO");
        CalculateMoney();
        RaceTime.text = time.ToString();
        Money.text = collectedMoney.ToString();
        coinScript.AddCoins(collectedMoney);

        StartCoroutine(PlayWinMusic(2));
       
    }

    private void CalculateMoney()
    {
        collectedMoney = 500 - (int)time; 
    }
    IEnumerator PlayWinMusic(int times)
    {
        for (int i = 0; i < times; i++)
        {
            winMusic.Play();
            yield return new WaitForSeconds(win.length);
        }
    }
}
