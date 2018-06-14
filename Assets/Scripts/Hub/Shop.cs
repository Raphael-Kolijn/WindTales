using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : TappableObject
{
	public ShopItem[] Items;
	public ShopItemUi ShopItemUi;
	public CoinScript CoinManager;
	
	void Start () {
		InitialiseTrigger();
	}

	public override void OpenUi()
	{
		AudioManager.PlaySound("MenuOpen");
		
		_uiInstance.GetComponent<ShopUi>().ClearShop();
		_uiInstance.GetComponent<ShopUi>().AudioManager = AudioManager;
		_uiInstance.SetActive(true);
		foreach (var shopItem in Items)
		{
			ShopItemUi itemUi = Instantiate(ShopItemUi);
			itemUi.SetData(shopItem);
			itemUi.AudioManager = AudioManager;
			itemUi.CoinManager = CoinManager;
			itemUi.shopUi = _uiInstance.GetComponent<ShopUi>();
			
			_uiInstance.GetComponent<ShopUi>().Add(itemUi);
		}
		
		_uiInstance.GetComponent<ShopUi>().LoadPage(1);
		_uiInstance.GetComponent<ShopUi>().SetCoinText(CoinManager.GetCoinTotal());
	}
}
