using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;
using UnityEngine.EventSystems;

public class OptionsScreen : Screens
{
    [Header("event System GameObjects")] [SerializeField]
    private GameObject options_1GameObject;
    [SerializeField] private GameObject resolutionGameObj;

    [Header("Graphics")]
    [SerializeField] TextMeshProUGUI resolutionValueText;

    [SerializeField] TextMeshProUGUI ecraValueText;

    [SerializeField] TextMeshProUGUI qualityValueText;

    [Header("Audio")]
    /* [SerializeField] TextMeshProUGUI sFXValueText;
 
     [SerializeField] TextMeshProUGUI musicValueText;*/
    [SerializeField]
    private Slider sfxSlider, musicSLider;
    
    [Header("Back Button")]
    [SerializeField]
    private ButtonToScreen backButton;

    [Header("Restore button")]
    [SerializeField]
    private Button restoreButton;

    [Header("Graphic Settings")]
    [SerializeField]
    private GraphicSettings graphicSettings;

    private void Awake()
    {
        backButton.button.onClick.AddListener(delegate { GoToScreen(backButton.goToScreen); });
        restoreButton.onClick.AddListener(ResetAllSettings);
        
        sfxSlider.onValueChanged.AddListener(delegate { ChangeSFXVolume((int)sfxSlider.value); });
        musicSLider.onValueChanged.AddListener(delegate { ChangeMusicVolume((int)musicSLider.value); });
    }

    void OnEnable()
    {
        if (InterfaceManager.Instance) InterfaceManager.Instance.OnResetAllSettings += ResetAllSettings;
        sfxSlider.maxValue = 10;
        musicSLider.maxValue = 10;
        
        EventSystem.current.SetSelectedGameObject(options_1GameObject);
        graphicSettings.LoadSettings();
    }

    void OnDisable()
    {
        if (InterfaceManager.Instance) InterfaceManager.Instance.OnResetAllSettings -= ResetAllSettings;
    }
    public void ChangeResolution(int value)
    {
        graphicSettings.ApplyResolution(value);
    }
    public void ChangeResolutionText(string resolution)
    {
        resolutionValueText.text = resolution;
    }

    public void ChangeEcra(int value)
    {
        graphicSettings.ApplyEcra(value);
    }
    public void ChangeEcraText(string ecra)
    {
        ecraValueText.text = ecra;
    }

    public void ChangeQuality(int value)
    {
        graphicSettings.ApplyQuality(value);
    }
    public void ChangeQualityText(string quality)
    {
        qualityValueText.text = quality;
    }

    public void ChangeSFXVolume(int value)
    {
        graphicSettings.ApplySFXSound(value);
    }
    public void ChangeSFXVolumeText(int sfx)
    {
        sfxSlider.value = sfx;
       // sFXValueText.text = sfx;
    }

    public void ChangeMusicVolume(int value)
    {
        graphicSettings.ApplyMusicSound(value);
    }

    public void ChangeMusicVolumeText(int music)
    {
        musicSLider.value = music;
        //musicValueText.text = music;
    }

    public override void GoToScreen(ScreensName screensName)
    {
        base.GoToScreen(screensName);
    }

    
    public void ResetAllSettings()
    {
        graphicSettings.ResetAllSettings();
    }
}