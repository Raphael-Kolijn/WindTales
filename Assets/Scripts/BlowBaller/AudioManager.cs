using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	// private AudioSource audiosource;
	public AudioClip pickUpSound;
    public AudioClip launchSound;
    public AudioClip endSound;
    public AudioClip springTick;
    public AudioSource wind;
    public AudioSource bgMusic;

    private int tickFrequency = 0;
    private int endCheck = 0;

	public void playPickupSound()
	{
		// Debug.Log ("AudioManager");
		AudioSource.PlayClipAtPoint (pickUpSound, transform.position);
	}

    public void playEndSound()
    {
        if(endCheck == 0)
        {
            AudioSource.PlayClipAtPoint(endSound, transform.position);
            endCheck = 1;
        }
        
    }

    public void playLaunchSound()
    {
        AudioSource.PlayClipAtPoint(launchSound, transform.position);
    }

    public void playSpringTick()
    {
        tickFrequency += 1;
        if(tickFrequency >= 10)
        {
            AudioSource.PlayClipAtPoint(springTick, transform.position);
            tickFrequency = 0;
        }   
    }

    public void startWind()
    {
        wind.Play();
    }

    public void stopWind()
    {
        wind.Stop();
    }

    public void stopMusic()
    {
        bgMusic.Stop();
    }
}