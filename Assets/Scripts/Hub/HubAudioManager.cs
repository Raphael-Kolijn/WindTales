using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class HubAudioManager : MonoBehaviour {

	public HubAudioFile[] sounds;
	public AudioMixerGroup mixerGroup;

	// Use this for initialization
	void Start () {
		foreach(var s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.outputAudioMixerGroup = mixerGroup;
		}
	}
	
	public void PlaySound(string soundName)
	{
		try
		{
			var sound = Array.Find(sounds, s => s.name == soundName);
			sound.source.Play();
		} 
		catch (Exception e)
		{
			Debug.LogWarning("Error playing sound: " + soundName + "\n " + "Are you missing an audiofile?");
		}
	}
}
