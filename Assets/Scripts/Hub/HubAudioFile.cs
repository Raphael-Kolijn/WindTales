using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HubAudioFile {


	public string name;
	public AudioClip clip;
	[HideInInspector]
	public AudioSource source;
}
