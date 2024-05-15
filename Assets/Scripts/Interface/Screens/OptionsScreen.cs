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
  
    private int sfxValue = 0;
   
   [SerializeField] TextMeshProUGUI musicValueText;
   private int musicValue = 0;
   
   [Header("Back Button")] [SerializeField]
   private ButtonToScreen backButton;
   
   [Header("Restore button")] [SerializeField]
   private Button restoreButton;

   [Header("Graphic Settings")] [SerializeField]
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
       //Pro Luan
       sfxValue += value;
       if (sfxValue >= 10)
       {
           sfxValue = 10;
           return;
       }
       if (sfxValue < 0)
       {
           sfxValue = 0;
           return;
       }
       sFXValueText.text = sfxValue.ToString();
       graphicSettings.SaveSettings("SFX",sfxValue);
       graphicSettings.ApplySFXSound(sfxValue);
   }

   public void ChangeMusicVolume(int value)
   {
       //Pro Luan
       musicValue += value;
       if (musicValue >= 10)
       {
           musicValue = 10;
           return;
       }
       if (musicValue < 0)
       {
           musicValue = 0;
           return;
       }
       
        musicValueText.text = musicValue.ToString();
        graphicSettings.SaveSettings("Music", musicValue);
        graphicSettings.ApplyMusicSound(musicValue);
   }
   public void UpdateSFX(int value)
   {
       sfxValue = value;
       sFXValueText.text = sfxValue.ToString();
   }
   public void UpdateMusic(int value)
   {
       musicValue = value;
       musicValueText.text = musicValue.ToString();
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
