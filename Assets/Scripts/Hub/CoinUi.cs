using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinUi : MonoBehaviour
{

	public CoinScript CoinScirpt;
	public Text CoinText;
	
	void Start () {
		CoinScirpt.CoinChangeEvent.AddListener(UpdateUi);
		UpdateUi();
	}
	
	private void UpdateUi()
	{
		CoinText.text = "x" + CoinScirpt.GetCoinTotal();
	}
}
