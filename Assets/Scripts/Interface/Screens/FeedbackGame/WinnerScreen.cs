using System;
using System.Collections;
using System.Collections.Generic;
using SceneSelect;
using UnityEngine;

public class WinnerScreen : Screens
{
    [SerializeField]private PlayerScore playerScore;
    [SerializeField] private ButtonToScreen initialScreenButton;

    private void Awake()
    {
        initialScreenButton.button.onClick.AddListener(delegate 
        { 
            GoToScreen(initialScreenButton.goToScreen);
            InterfaceManager.Instance.startNewGame = false;
            InterfaceManager.Instance.inGame = false;
            ScenesManager.Instance.UnloadCurrentScene();         
            
        });
    }

    public override void Initialize()
    {
        base.Initialize();

        PlayersManager.PlayerConfigurationData p = new PlayersManager.PlayerConfigurationData();
        foreach (var w in InterfaceManager.Instance.playerScores)
        {
            if (w.score >=PlayersManager.Instance.ScoreToWinGame)
                p = w;
        }

        playerScore.ChangeData(p);
    }
    
}
