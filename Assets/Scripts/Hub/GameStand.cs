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

	public Popup ClosedPopup;
	public Popup LockedPopup;
	
	// Use this for initialization
	void Start () {
		InitialiseTrigger();
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
		try
		{
			SceneManager.LoadScene(GameScene);
		}
		catch (Exception e)
		{
			Debug.LogError(e.Message);			
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

	public void OpenUi()
	{

		if (IsOpen && IsUnlocked)
		{
			_uiInstance.GetComponent<GameStandUi>().SetInfo(this);
			_uiInstance.SetActive(true);
		} 
		else if (!IsOpen)
		{
			ClosedPopup.Animator.SetBool("PlayAnimation", true);
		} 
		else if (!IsUnlocked)
		{
			LockedPopup.Animator.SetBool("PlayAnimation", true);	
		}
	}
}
