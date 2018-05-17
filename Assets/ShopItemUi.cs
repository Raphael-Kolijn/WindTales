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
	private ShopItem myItem;

	public void SetData(ShopItem item)
	{
		myItem = item;
		ItemTitle.text = item.ItemName;
		Thumbnail.sprite = item.thumbnail;
		BuyText.text = item.GetCosts().ToString();
		BuyButton.onClick.AddListener(Buy);
	}

	private void Buy()
	{
		myItem.Unlock();
	}
}
