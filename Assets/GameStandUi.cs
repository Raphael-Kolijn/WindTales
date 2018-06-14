using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStandUi : MonoBehaviour
{

	public Text Title;
	public Image Thumbnail;
	public GameStand game;

	public void SetInfo(GameStand game)
	{
		this.game = game;
		Title.text = game.Name;
		Thumbnail.sprite = game.Thumbnail;
	}

	public void Play()
	{
		game.StartGame();
	}
}
