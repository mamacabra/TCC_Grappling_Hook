using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeedbackGame : Screens
{
    [SerializeField] private Button nextGameButton;

    private void Awake()
    {
        nextGameButton.onClick.AddListener(NextGame);
    }
    void NextGame()
    {
        PlayersManager.Instance.InitGame(true);
    }
}
