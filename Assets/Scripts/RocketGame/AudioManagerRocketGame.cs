using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerRocketGame : MonoBehaviour {
    public AudioClip CoinCollectSound;
    public AudioClip MagnetSuctionSound;
    public AudioSource BackgroundMusic;

    public void PlayCoinCollect()
    {
        AudioSource.PlayClipAtPoint(CoinCollectSound, transform.position);
    }

    public void PlayMagnetSound()
    {
        AudioSource.PlayClipAtPoint(MagnetSuctionSound, transform.position);
    }

    public void PlayBackgroundMusic()
    {
        BackgroundMusic.Play();
    }
}
