using UnityEngine;
using UnityEngine.Events;

public class CoinScript : MonoBehaviour
{
	private const string Cointotal = "CoinTotal";
	[SerializeField] private int _mCoinTotal;
	public UnityEvent CoinChangeEvent;


	private void Awake()
	{
		CoinChangeEvent = new UnityEvent();
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
		CoinChangeEvent.Invoke();
	}

	/// <summary>
	/// Remove an amount of coins.
	/// Use a postive integer!
	/// </summary>
	/// <param name="amount">Non negative integer</param>
	public void RemoveCoins(int amount)
	{
		if (amount < 0)
		{
			Debug.LogWarning("Cannot remove a negative value");
			return;
		}
		
		_mCoinTotal -= amount;
		PlayerPrefs.SetInt(Cointotal, _mCoinTotal);		
		CoinChangeEvent.Invoke();
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
