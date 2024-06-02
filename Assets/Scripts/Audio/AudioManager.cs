using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class AudioManager : MonoBehaviour
{
    public static AudioManager audioManager;

    private void Awake()
    {
        if (audioManager != null && audioManager != this)
        {
            Destroy(gameObject);
        }
        else
        {
            
            audioManager = this;
            
        }
        DontDestroyOnLoad(this);
    }

    public void PlayUiSoundEffect(string uiSound)
    {
        RuntimeManager.PlayOneShot("event:/MenuEffects/UiSounds/" + uiSound);
    }

    public void PlayUiSoundEffect(SoundsList uiSound)
    {
        RuntimeManager.PlayOneShot("event:/MenuEffects/UiSounds/" + uiSound);
    }
}

public enum SoundsList { None, Confirm, Return, Cancel, Select, SelectUpPitch, SelectDownPitch};
