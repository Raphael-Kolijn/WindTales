using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
	private const string Cointotal = "CoinTotal";
	private int _mCoinTotal;


	public void Start()
	{
		_mCoinTotal = PlayerPrefs.GetInt(Cointotal, 0);
	}

	public void AddCoins(int amount)
	{
		if (amount < 0)
		{
			Debug.LogWarning("Cannot add a negative value");
			return;
		}
		
		_mCoinTotal += amount;
		PlayerPrefs.SetInt(Cointotal, _mCoinTotal);
	}

	/// <summary>
	/// Remove an amount of coins.
	/// Use a postive integer!
	/// </summary>
	/// <param name="amount">Non negative integer</param>
	private void RemoveCoins(int amount)
	{
		if (amount < 0)
		{
			Debug.LogWarning("Cannot remove a negative value");
			return;
		}
		
		_mCoinTotal -= amount;
		PlayerPrefs.SetInt(Cointotal, _mCoinTotal);		
	}

	public bool Pay(int amount)
	{
		if (amount < 0)
		{
			Debug.LogWarning("Cannot pay a negative value");
			return false;
		}
		
		if (_mCoinTotal < amount)
		{
			return false;
		}
		
		RemoveCoins(amount);
		return true;
	}

	public int GetCoinTotal()
	{
		return _mCoinTotal;
	}
}
