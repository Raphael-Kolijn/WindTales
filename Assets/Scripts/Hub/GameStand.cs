using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStand : TappableObject
{
    public bool IsUnlocked;
    public bool IsOpen;
    public string Name;
    public string GameScene;
    public Sprite Thumbnail;
    [Range(1, 15)] public int MaxDailyPlayCount;

    public Popup ClosedPopup;
    public Popup LockedPopup;

    private PlayCountPersistor playCount;

    // Use this for initialization
    void Start()
    {
        playCount = new PlayCountPersistor(Name);
        InitialiseTrigger();
        if (playCount.GetPlayCount() <= MaxDailyPlayCount)
        {
            IsOpen = true;
        }
        else
        {
            IsOpen = false;            
        }
        
        ShowPopup(ClosedPopup, false);

        if (IsUnlocked)
        {
            ShowPopup(LockedPopup, false);            
            if (IsOpen)
            {
                ShowPopup(ClosedPopup, false);
            }
            else
            {
                ShowPopup(ClosedPopup, true);
            }
        }
        else
        {
            ShowPopup(LockedPopup, true);
        }
    }

    public void StartGame()
    {
        if (playCount.GetPlayCount() <= MaxDailyPlayCount)
        {
            try
            {
                playCount.IncreasePlayCount();
                SceneManager.LoadScene(GameScene);
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }
        else
        {
            Debug.Log("Game has been played too many times");
        }
    }

    private void ShowPopup(Popup popup, bool state)
    {
        if (state)
        {
            popup.gameObject.SetActive(true);
        }
        else
        {
            popup.gameObject.SetActive(false);
        }
    }

    public new void OpenUi()
    {
        if (IsUnlocked && IsOpen)
        {
            _uiInstance.GetComponent<GameStandUi>().SetInfo(this);
            _uiInstance.SetActive(true);
        } 
        else if (!IsUnlocked)
        {
            LockedPopup.IncreasePopup();
        }
        else
        {
            ClosedPopup.IncreasePopup();
        }
    }
}