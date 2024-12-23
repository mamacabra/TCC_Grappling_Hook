using System.Collections.Generic;
using UnityEngine;
using Screen = UnityEngine.Device.Screen;

public class GraphicSettings : MonoBehaviour
{
    [Header("Options Screen")] [SerializeField]
    private OptionsScreen optionsScreen;

    [Header("Resolutions")] [SerializeField]
    List<Vector2> resolutions;
    private int resolutionValue = 0;

    [Header("Ecra")] [SerializeField] private List<string> ecra;
    private int ecraValue = 0;
    
    [Header("Quality")] [SerializeField] private List<string> quality;
    private int qualityValue = 2;

    private int sfxVolume = 5;
    private int musicVolume = 5;

    private void Start()
    {
        LoadSettings();
    }

    public void LoadSettings()
    {
        Resolution[] res = Screen.resolutions;
        resolutions.Clear();
        for(int i = 0; i < res.Length; i++)
            resolutions.Add(new Vector2(res[i].width, res[i].height));
        
        int r = PlayerPrefs.GetInt("Resolution", Screen.currentResolution.width);
        int screenMode = PlayerPrefs.GetInt("ScreenEcra", 0);
        int quality = PlayerPrefs.GetInt("Quality", QualitySettings.GetQualityLevel());
        int sfx = PlayerPrefs.GetInt("SFX", 5);
        int music = PlayerPrefs.GetInt("Music", 5);

        ApplyResolution(r);
        SetEcra(screenMode);
        SetQuality(quality);// @TODO: fix this.

        AudioManager.audioManager.SetSFXVolume(sfx);
        optionsScreen.ChangeSFXVolumeText(sfx);
        SaveSettings("SFX", sfx);

        AudioManager.audioManager.SetMusicVolume(music);
        optionsScreen.ChangeMusicVolumeText(music);
        SaveSettings("Music", music);
    }
    
    public void SaveSettings(string s, int value)
    {
        PlayerPrefs.SetInt(s, value);
        PlayerPrefs.Save();
    }
    public void ApplyResolution(int value)
    {
        resolutionValue += value;
        if (resolutionValue >= resolutions.Count)
        {
            resolutionValue = resolutions.Count-1;
            return;
        }
        if (resolutionValue < 0)
        {
            resolutionValue = 0;
            return;
        }

        Vector2 selectedResolution = resolutions[resolutionValue];
        Screen.SetResolution((int) selectedResolution.x, (int) selectedResolution.y, Screen.fullScreen);

        int rx = (int) selectedResolution.x;
        int ry = (int) selectedResolution.y;
        optionsScreen.ChangeResolutionText(rx+"x"+ry);

        SaveSettings("Resolution",resolutionValue);
    }

    public void ApplyEcra(int value)
    {
        ecraValue += value;
        if (ecraValue >= ecra.Count)
        {
            ecraValue = ecra.Count-1;
            return;
        }

        if (ecraValue < 0)
        {
            ecraValue = 0;
            return;
        }
       
        switch (ecraValue)
        {
            case 0:
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                break;
            case 1:
                Screen.fullScreenMode = FullScreenMode.Windowed;
                break;
        }

        optionsScreen.ChangeEcraText(ecra[ecraValue]);
        
        SaveSettings("ScreenEcra", ecraValue);
    }

    public void SetEcra(int value){
        ecraValue = value;

        switch (ecraValue)
        {
            case 0:
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                break;
            case 1:
                Screen.fullScreenMode = FullScreenMode.Windowed;
                break;
        }

        optionsScreen.ChangeEcraText(ecra[ecraValue]);
        
        SaveSettings("ScreenEcra", ecraValue);
    }

    public void ApplyQuality(int value)
    {
        qualityValue += value;
        if (qualityValue >= quality.Count)
        {
            qualityValue = quality.Count-1;
            return;
        }

        if (qualityValue < 0)
        {
            qualityValue = 0;
            return;
        }
       
        
        QualitySettings.SetQualityLevel(qualityValue);
        optionsScreen.ChangeQualityText(quality[qualityValue]);

        SaveSettings("Quality", qualityValue);
    }

    public void SetQuality(int value)
    {
        qualityValue = value;

        QualitySettings.SetQualityLevel(qualityValue);
        optionsScreen.ChangeQualityText(quality[qualityValue]);

        SaveSettings("Quality", qualityValue);
    }

    public void ApplySFXSound(int value)
    {
        AudioManager.audioManager.ChangeSfxVolume(value);
        sfxVolume = (int)AudioManager.audioManager.sfxVolume;
        optionsScreen.ChangeSFXVolumeText(sfxVolume);
        SaveSettings("SFX", sfxVolume);
    }
    public void ApplyMusicSound(int value)
    {
        AudioManager.audioManager.ChangeMusicVolume(value);
        musicVolume = (int)AudioManager.audioManager.musicVolume;
        optionsScreen.ChangeMusicVolumeText(musicVolume);
        SaveSettings("Music", musicVolume);
    }
    public void ResetAllSettings()
    {
        resolutionValue = resolutions.Count-1;
        ecraValue = 0;
        qualityValue = quality.Count-1;
        sfxVolume = 5;
        musicVolume = 5;
        
        Vector2 selectedResolution = resolutions[resolutionValue];
        Screen.SetResolution((int) selectedResolution.x, (int) selectedResolution.y, Screen.fullScreen);
        
        int rx = (int) selectedResolution.x;
        int ry = (int) selectedResolution.y;
        optionsScreen.ChangeResolutionText(rx+"x"+ry);
        SaveSettings("Resolution", resolutionValue);

        Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        optionsScreen.ChangeEcraText(ecra[ecraValue]);
        SaveSettings("ScreenEcra", ecraValue);
        
        QualitySettings.SetQualityLevel(qualityValue);
        optionsScreen.ChangeQualityText(quality[qualityValue]);
        SaveSettings("Quality", qualityValue);

        AudioManager.audioManager.SetSFXVolume(sfxVolume);
        optionsScreen.ChangeSFXVolumeText(sfxVolume);
        SaveSettings("SFX", sfxVolume);

        AudioManager.audioManager.SetMusicVolume(musicVolume);
        optionsScreen.ChangeMusicVolumeText(musicVolume);
        SaveSettings("Music", musicVolume);
    }
}
