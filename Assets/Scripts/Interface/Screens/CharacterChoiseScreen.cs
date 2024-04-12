using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterChoiseScreen : Screens
{
    [SerializeField] private ButtonToScreen backToMenu, playGame;
    public Transform charactersGroup;
    
    private void Awake()
    {
        backToMenu.button.onClick.AddListener(delegate { GoToScreen(backToMenu.goToScreen); });
        playGame.button.onClick.AddListener(delegate { GoToScreen(playGame.goToScreen); });
    }

    public override void Initialize()
    {
        if (PlayersManager.Instance)
        {
            PlayersManager.Instance.characterChoice = this;
            PlayersManager.Instance.InitCharacterSelection();
        }
    }
    public override void Close()
    {
        if (PlayersManager.Instance)
        {
            PlayersManager.Instance.DisableInputActions();
        }
    }

    public override void GoToScreen(ScreensName screensName)
    {
        if (PlayersManager.Instance.CanInitGame)
        {
            base.GoToScreen(screensName);
        }
    }
}
