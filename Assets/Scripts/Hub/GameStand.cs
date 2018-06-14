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
    public GameStand PreviousGame;

    public Popup ClosedPopup;
    public Popup LockedPopup;
    public PlayCountPersistor playCount;

    public LevelChanger LevelChanger;

    public ParticleSystem Particles;


    private void Awake()
    {
        playCount = new PlayCountPersistor(Name);
        Particles.Stop();
    }

    // Use this for initialization
    void Start()
    {
        InitialiseTrigger();        

        ShowPopup(ClosedPopup, false);
        
        if (PreviousGame == null)
        {
            IsUnlocked = true;
        }
        else
        {
            if (PreviousGame.playCount.GetTotalPlayCount() > 1)
            {
                IsUnlocked = true;
            }
            else
            {
                IsUnlocked = false;
            }     
        }

        if (playCount.GetPlayCount() <= MaxDailyPlayCount)
        {
            IsOpen = true;
        }
        else
        {
            IsOpen = false;
        }

        if (IsUnlocked)
        {
            ShowPopup(LockedPopup, false);
            if (IsOpen)
            {
                ShowPopup(ClosedPopup, false);
                Particles.Play();
            }
            else
            {
                ShowPopup(ClosedPopup, true);
                Particles.Stop();
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
                LevelChanger.FadeToLevel(GameScene);
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

    public override void OpenUi()
    {
        if (IsOpen && IsUnlocked)
        {
            AudioManager.PlaySound("MenuOpen");
            _uiInstance.GetComponent<GameStandUi>().SetInfo(this);
            _uiInstance.GetComponent<GameStandUi>().AudioManager = AudioManager;
            _uiInstance.SetActive(true);
        }
        else if (!IsUnlocked)
        {
            LockedPopup.Animator.SetBool("PlayAnimation", true);
        }
        else if (!IsOpen)
        {
            ClosedPopup.Animator.SetBool("PlayAnimation", true);
        }
    }
}