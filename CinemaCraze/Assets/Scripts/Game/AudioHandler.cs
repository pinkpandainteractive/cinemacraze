using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    public AudioClip buyUpgrades;
    public AudioClip fail;
    public AudioClip click;
    
    public void PlayFail()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.volume = 0.2f;
        audio.clip = fail;
        audio.Play();

    }
    public void PlayBuyUpgrades()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.volume = 0.2f;
        audio.clip = buyUpgrades;
        audio.Play();

    }
    public void PlayClick()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.volume = 1.0f;
        audio.clip = click;
        audio.Play();
    }
}
