using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemUi : MonoBehaviour
{
	public Text ItemTitle;
	public Image Thumbnail;
	public Button BuyButton;
	public Text BuyText;
	private ShopItem _myItem;
	public CoinScript CoinManager;
	public ShopUi shopUi;
	public HubAudioManager AudioManager;

	public void SetData(ShopItem item)
	{
		_myItem = item;
		ItemTitle.text = item.ItemName;
		Thumbnail.sprite = item.Thumbnail;
		BuyText.text = item.GetCosts().ToString();
		
		if (_myItem.IsUnlocked())
		{
			BuyButton.interactable = false;
		}
		else
		{
			BuyButton.onClick.AddListener(Buy);	
		}		
	}

	private void Buy()
	{
		if (CoinManager.Pay(_myItem.GetCosts()))
		{
			BuyButton.interactable = false;
			_myItem.Unlock();
			shopUi.SetCoinText(CoinManager.GetCoinTotal());
			AudioManager.PlaySound("Buy");
		}
		else
		{
			// Play sound or some shit.
			AudioManager.PlaySound("BuyError");
		}
		
	}
}
