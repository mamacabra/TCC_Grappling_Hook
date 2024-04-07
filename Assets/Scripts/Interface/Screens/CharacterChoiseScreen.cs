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
        playGame.button.onClick.AddListener(delegate { TryInitGame(); });
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
            PlayersManager.Instance.Disable();
        }
    }

    public override void GoToScreen(ScreensName screensName)
    {
        base.GoToScreen(screensName);
    }

    private void TryInitGame(){
        if (PlayersManager.Instance.CanInitGame) {
            GoToScreen(playGame.goToScreen);
        }
    }
}
