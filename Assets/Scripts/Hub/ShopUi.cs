using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.UI;

public class ShopUi : MonoBehaviour
{

	public GameObject ShopContentPanel;
	public Text PageText;
	public Text CoinText;
	private List<ShopItemUi> items = new List<ShopItemUi>();
	private int _currentPage = 1;
	private int _itemCounter = 0;
	private int _itemPerPage = 3;
	public HubAudioManager AudioManager;

	public void Add(ShopItemUi item)
	{
		items.Add(item);
		_itemCounter++;

		item.transform.SetParent(ShopContentPanel.transform, false);	
				
		SetPageNumber();
	}

	public void ClearShop()
	{
		for (int i = 0; i < ShopContentPanel.transform.childCount; i++)
		{
			Destroy(ShopContentPanel.transform.GetChild(i).gameObject);
		}

		_itemCounter = 0;
		items.Clear();
		_currentPage = 1;
	}

	public void NextPage()
	{
		if (_currentPage == (_itemCounter + _itemPerPage - 1) / _itemPerPage)
		{
			return;
		}
		
		AudioManager.PlaySound("ButtonSwoosh");
		_currentPage++;
		LoadPage(_currentPage);
		SetPageNumber();
	}

	public void PreviousPage()
	{
		if (_currentPage == 1)
		{
			return;
		}
		
		AudioManager.PlaySound("ButtonSwoosh");
		_currentPage--;
		LoadPage(_currentPage);		
		SetPageNumber();
	}

	private void SetPageNumber()
	{
		int pages  = (_itemCounter + _itemPerPage - 1) / _itemPerPage;

		PageText.text = _currentPage + "/" + pages;
	}

	public void LoadPage(int index)
	{
		for (int i = 0; i < _itemCounter; i++) 
		{
			if (index == 1)
			{
				if (i == 0 || i == 1 || i == 2)
				{
					items[i].gameObject.SetActive(true);
				}
				else
				{
					items[i].gameObject.SetActive(false);				
				}
			}
			else
			{
				if (i == (index -1 ) * _itemPerPage || i == (index -1 ) * _itemPerPage + 1 || i == (index -1 ) * _itemPerPage + 2)
				{
					items[i].gameObject.SetActive(true);
				}
				else
				{
					items[i].gameObject.SetActive(false);				
				}
			}
		}
	}

	public void SetCoinText(int amount)
	{
		CoinText.text = "x" + amount;
	}
	
	public void PlayAudio(string name)
	{
		AudioManager.PlaySound(name);
	}
}
