using System;
using System.Collections;
using System.Collections.Generic;
using Character;
using Character.Utils;
using LocalMultiplayer;
using LocalMultiplayer.Data;
using SceneSelect;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseInGameScreen : Screens
{
    [SerializeField] private ButtonToScreen backToMenu;
    [SerializeField] private ButtonToScreen continueButton;

    [SerializeField] private TextMeshProUGUI pauseText;
    [SerializeField] private Image line;

    private void Awake()
    {
        continueButton.button.onClick.AddListener(Continue);
        backToMenu.button.onClick.AddListener(QuitGame);
    }

    public override void Initialize()
    { 
        base.Initialize();
        EventSystem.current.SetSelectedGameObject(continueButton.button.gameObject);
        Time.timeScale = 0;
        List<PlayerConfigurationData> list = new List<PlayerConfigurationData>();
        list = PlayersManager.Instance.ReturnPlayersList();

        pauseText.color = new Color32(240, 240, 240, 255);
        line.color = new Color32(240, 240, 240, 255);
        if (InterfaceManager.Instance.currentDevice != null)
        {
            PlayerConfigurationData a = list.Find(d => d.inputDevices[0] == InterfaceManager.Instance.currentDevice);
            
            var col = PlayerColorLayerManager.GetColorBaseLight(a.id);
            pauseText.color = col;
            line.color = col;
        }
        else {
            pauseText.color = Color.white;
            line.color = Color.white;
        }
    }
    
    public override void GoToScreen(ScreensName screensName)
    {
        base.GoToScreen(screensName);
    }

    public void QuitGame()
    {
        ScenesManager.Instance.ReloadMainScene();
        InterfaceManager.Instance.inGame = false;
        GoToScreen(backToMenu.goToScreen);
        Time.timeScale = 1;
    }
    
    public override void Close()
    {
        //base.Close();
        //GoToScreen(continueButton.goToScreen);
        gameObject.SetActive(false);
        // Time.timeScale = 1;
    }
    
    public void Continue()
    {
        //base.Close();
        GoToScreen(continueButton.goToScreen);
        gameObject.SetActive(false);
        // Time.timeScale = 1;
    }
}
