using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{

	public Animator animator;

	private string lvlToLoad;

	public void FadeToLevel(string lvlName)
	{
		lvlToLoad = lvlName;
		animator.SetTrigger("FadeOut");
	}

	public void OnFadeComplete()
	{
		SceneManager.LoadScene(lvlToLoad);
	}
}
