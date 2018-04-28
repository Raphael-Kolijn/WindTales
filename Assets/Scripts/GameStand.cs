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

	public GameObject ClosedPopup;
	public GameObject LockedPopup;
	
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
	
	private void ShowPopup(GameObject popup, bool state)
	{
		if (state)
		{
			popup.SetActive(true);
		}
		else
		{
			popup.SetActive(false);
		}
	}
}
