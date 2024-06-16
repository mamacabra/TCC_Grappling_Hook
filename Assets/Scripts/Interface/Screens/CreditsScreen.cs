using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CreditsScreen : Screens
{
    [SerializeField] private ButtonToScreen backButton;
    [SerializeField] private GameObject handler;

    private void Awake()
    {
        backButton.button.onClick.AddListener(delegate { GoToScreen(backButton.goToScreen); });
    }

    public override void Initialize()
    {
        base.Initialize();
        EventSystem.current.SetSelectedGameObject(handler);
    }
    public override void GoToScreen(ScreensName screensName)
    {
        base.GoToScreen(screensName);
    }
}
