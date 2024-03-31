using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterChoiseScreen : Screens
{
    [SerializeField] private ButtonToScreen backToMenu, playGame;
    
    private void Awake()
    {
        backToMenu.button.onClick.AddListener(delegate { GoToScreen(backToMenu.goToScreen); });
        playGame.button.onClick.AddListener(delegate { GoToScreen(playGame.goToScreen); });
    }

    public override void GoToScreen(ScreensName screensName)
    {
        base.GoToScreen(screensName);
    }

}
