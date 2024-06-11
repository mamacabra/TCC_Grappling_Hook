using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class AudioManager : MonoBehaviour
{
    public static AudioManager audioManager;

    [Header("Press M to play the selected sound")]
    [SerializeField] PlayerSoundsList soundTest;
    [SerializeField] bool debugActivated;
    
    [SerializeField]
    private string musicBusPath;
    FMOD.Studio.Bus musicBus;
    public float musicVolume;

    [SerializeField]
    private string sfxBusPath;
    FMOD.Studio.Bus sfxBus;
    public float sfxVolume;

    private void Awake()
    {
        if (audioManager != null && audioManager != this)
        {
            Destroy(gameObject);
        }
        else
        {       
            audioManager = this;
            musicBus = RuntimeManager.GetBus(musicBusPath);
            sfxBus = RuntimeManager.GetBus(sfxBusPath);
        }
        DontDestroyOnLoad(this);
    }

    public void PlayUiSoundEffect(string uiSound)
    {
        RuntimeManager.PlayOneShot("event:/MenuEffects/UiSounds/" + uiSound);
    }

    public void PlayUiSoundEffect(UiSoundsList uiSound)
    {
        RuntimeManager.PlayOneShot("event:/MenuEffects/UiSounds/" + uiSound);
    }

    public void PlayPlayerSoundEffect(PlayerSoundsList sound)
    {
        RuntimeManager.PlayOneShot("event:/PlayerEffects/" + sound);
    }

    public void ChangeMusicVolume(int value)
    {
        musicVolume += value;         
        if (musicVolume >= 10)
        {
            musicVolume = 10;
            return;
        }
        if (musicVolume < 0)
        {
            musicVolume = 0;     
            return;
        }
        musicBus.setVolume(musicVolume / 10);
    }

    public void SetMusicVolume(int value) {
        musicVolume = value;
        musicBus.setVolume(musicVolume / 10);
    }

    private void Update()
    {
        if (debugActivated)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                PlayPlayerSoundEffect(soundTest);
            }
        }      
    }

    public void ChangeSfxVolume(int value)
    {
        sfxVolume += value;
        if (sfxVolume >= 10)
        {
            sfxVolume = 10;
            return;
        }
        if (sfxVolume < 0)
        {
            sfxVolume = 0;
            return;
        }
        sfxBus.setVolume(sfxVolume / 10);

    }
    public void SetSFXVolume(int value) {
        sfxVolume = value;
        sfxBus.setVolume(musicVolume / 10);
    }
    
}

public enum UiSoundsList { None, Confirm, Return, Cancel, Select, SelectUpPitch, SelectDownPitch};
public enum PlayerSoundsList { HookCharge, HookFire, HookHitPlayer, HookHitWall, HookReturn, AttackParry, AttackMiss, AttackHitPlayer};
