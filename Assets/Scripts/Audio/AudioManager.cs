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
    FMOD.Studio.EventInstance gameMusic;

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
            ChangeMusic();
        }
        DontDestroyOnLoad(this);
    }

    public void ChangeMusic()
    {
        gameMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        string music;
        int aux = Random.Range(1, 3 + 1);
        switch (aux)
        {
            case 1:
                music = MusicList.Music1.ToString();
                break;

            case 2:
                music = MusicList.Music2.ToString();
                break;

            case 3:
                music = MusicList.Music3.ToString();                
                break;

            default:
                music = MusicList.Music1.ToString();
                break;

        }
        gameMusic = RuntimeManager.CreateInstance("event:/Musics/" + music);
        gameMusic.start();
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
            if (Input.GetKeyDown(KeyCode.N))
            {
                ChangeMusic();
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
        sfxBus.setVolume(sfxVolume / 10);
    }
    
}

public enum UiSoundsList { None, Confirm, Return, Cancel, Select, SelectUpPitch, SelectDownPitch};
public enum PlayerSoundsList { HookCharge, HookFire, HookHitPlayer, HookHitWall, HookReturn, AttackParry, AttackMiss, AttackHitPlayer, PlayerDash, PlayerSpawn, PlayerWalk};
public enum MusicList { Music1, Music2, Music3};
