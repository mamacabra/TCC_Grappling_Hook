using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsScreen : Screens
{
    [SerializeField] private ButtonToScreen backButton;

    private void Awake()
    {
        backButton.button.onClick.AddListener(delegate { GoToScreen(backButton.goToScreen); });
    }

    public override void GoToScreen(ScreensName screensName)
    {
        base.GoToScreen(screensName);
    }
}
