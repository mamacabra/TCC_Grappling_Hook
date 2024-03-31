using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseInGameScreen : Screens
{
    [SerializeField] private ButtonToScreen backToMenu;
    [SerializeField] private Button continueButton;

    private void Awake()
    {
        continueButton.onClick.AddListener(Close);
        backToMenu.button.onClick.AddListener(delegate { GoToScreen(backToMenu.goToScreen); });
    }

    public override void Initialize()
    { 
       base.Initialize();
       Time.timeScale = 0;
    }
    
    public override void GoToScreen(ScreensName screensName)
    {
        base.GoToScreen(screensName);
    }
    
    public override void Close()
    {
        //base.Close();
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
