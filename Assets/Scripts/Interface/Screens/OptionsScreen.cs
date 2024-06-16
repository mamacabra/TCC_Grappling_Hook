using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;

public class OptionsScreen : Screens
{
    [Header("Graphics")]
    [SerializeField] TextMeshProUGUI resolutionValueText;

    [SerializeField] TextMeshProUGUI ecraValueText;

    [SerializeField] TextMeshProUGUI qualityValueText;

    [Header("Audio")]
    [SerializeField] TextMeshProUGUI sFXValueText;

    [SerializeField] TextMeshProUGUI musicValueText;
    
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
    }

    void OnEnable()
    {
        graphicSettings.LoadSettings();
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
    public void ChangeSFXVolumeText(string sfx)
    {
        sFXValueText.text = sfx;
    }

    public void ChangeMusicVolume(int value)
    {
        graphicSettings.ApplyMusicSound(value);
    }

    public void ChangeMusicVolumeText(string music)
    {
        musicValueText.text = music;
    }

    public override void GoToScreen(ScreensName screensName)
    {
        base.GoToScreen(screensName);
    }

    void ResetAllSettings()
    {
        graphicSettings.ResetAllSettings();
    }
}