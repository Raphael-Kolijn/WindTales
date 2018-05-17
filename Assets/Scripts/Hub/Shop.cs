using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : TappableObject
{
	public ShopItem[] items;
	public GameObject shopItemUi;
	
	void Start () {
		InitialiseTrigger();
	}

	public void OpenUi()
	{
		_uiInstance.SetActive(true);
		foreach (var shopItem in items)
		{
			
		}
	}
}
