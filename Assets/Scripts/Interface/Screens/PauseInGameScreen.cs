using System;
using System.Collections;
using System.Collections.Generic;
using SceneSelect;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseInGameScreen : Screens
{
    [SerializeField] private ButtonToScreen backToMenu;
    [SerializeField] private ButtonToScreen continueButton;

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
    }
    
    public override void GoToScreen(ScreensName screensName)
    {
        base.GoToScreen(screensName);
    }

    public void QuitGame()
    {
        ScenesManager.Instance.UnloadCurrentScene();
        InterfaceManager.Instance.inGame = false;
        GoToScreen(backToMenu.goToScreen);
    }
    
    public override void Close()
    {
        //base.Close();
        //GoToScreen(continueButton.goToScreen);
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
    
    public void Continue()
    {
        //base.Close();
        GoToScreen(continueButton.goToScreen);
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
