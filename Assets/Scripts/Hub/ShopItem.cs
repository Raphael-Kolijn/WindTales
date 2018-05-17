using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{

	[SerializeField] private bool _isUnlocked = false;
	public string ItemName;
	[SerializeField] private int _costs;
	public Sprite thumbnail;
	
	void Start () {
		if (PlayerPrefs.GetInt(gameObject.name, 0) == 1)
		{
			_isUnlocked = true;
		}
		else
		{
			_isUnlocked = false;
		}
		gameObject.SetActive(_isUnlocked);
	}
	
	public void Unlock()
	{
		PlayerPrefs.SetInt(gameObject.name, 1);
		_isUnlocked = true;
		gameObject.SetActive(_isUnlocked);
	}

	public int GetCosts()
	{
		return _costs;
	}
}
