using System;
using System.Collections;
using System.Collections.Generic;
using SceneSelect;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WinnerScreen : Screens
{
   // [SerializeField]private PlayerScore playerScore;
    [SerializeField] private ButtonToScreen initialScreenButton;
    [SerializeField] private Image winner;
    private void Awake()
    {
        initialScreenButton.button.onClick.AddListener(delegate 
        { 
            GoToScreen(initialScreenButton.goToScreen);
            InterfaceManager.Instance.startNewGame = false;
            InterfaceManager.Instance.inGame = false;
            //InterfaceManager.Instance.playerScores.Clear();
            ScenesManager.Instance.UnloadCurrentScene();         
            
        });
    }

    public override void Initialize()
    {
        base.Initialize();
        List<PlayersManager.PlayerConfigurationData> list = new List<PlayersManager.PlayerConfigurationData>();
        list = PlayersManager.Instance.ReturnPlayersList();
        
        foreach (var w in list)
        {
            if (w.score >= PlayersManager.Instance.ScoreToWinGame)
            {
                winner.sprite = Resources.Load<ResourcesCharacters>("ResourcesCharacters").GetCharacterData(w.characterModel).characterSprite;
            }
        }

       
        //playerScore.ChangeData(p);
        EventSystem.current.SetSelectedGameObject(initialScreenButton.button.gameObject);
    }
    
}