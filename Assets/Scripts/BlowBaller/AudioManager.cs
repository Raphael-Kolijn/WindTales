using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	// private AudioSource audiosource;
	public AudioClip pickUpSound;

	public void playPickupSound()
	{
		// Debug.Log ("AudioManager");
		AudioSource.PlayClipAtPoint (pickUpSound, transform.position);
	}
}
