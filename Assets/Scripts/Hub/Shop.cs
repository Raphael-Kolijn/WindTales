using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : TappableObject
{
	public ShopItem[] Items;
	public ShopItemUi ShopItemUi;
	
	void Start () {
		InitialiseTrigger();
	}

	public new void OpenUi()
	{
		_uiInstance.SetActive(true);
		foreach (var shopItem in Items)
		{
			ShopItemUi itemUi = Instantiate(ShopItemUi);
			itemUi.SetData(shopItem);
			itemUi.transform.SetParent(_uiInstance.transform, false);
		}
	}
}
