using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeedbackGame : Screens
{
    [SerializeField] private Button nextGameButton;

    public override void Initialize()
    {
        InterfaceManager.Instance.OnHideButton += HideButton;
    }

    public override void Close()
    {
        base.Close();
        if(!InterfaceManager.Instance) return;
        InterfaceManager.Instance.OnHideButton -= HideButton;
        
    }
    private void Awake()
    {
        nextGameButton.onClick.AddListener(NextGame);
    }
    void NextGame()
    {
        PlayersManager.Instance.InitGame(true);
        Close();
    }

    public override void HideButton()
    {
        nextGameButton.gameObject.SetActive(false);
    }
}
