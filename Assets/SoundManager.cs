using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SoundManager : MonoBehaviour
{
    [SerializeField]
    Sprite musicMuted;
    [SerializeField]
    Sprite musicUnmuted;
    [SerializeField]
    private Button soundButton;
    public void ToggleSound()
    {
        if (AudioListener.volume == 0f)
        {
            soundButton.image.sprite = musicUnmuted;
            AudioListener.volume = 1f;
        }
        else if (AudioListener.volume == 1f)
        {
            soundButton.image.sprite = musicMuted;
            AudioListener.volume = 0f;
        }
    }
    
}
