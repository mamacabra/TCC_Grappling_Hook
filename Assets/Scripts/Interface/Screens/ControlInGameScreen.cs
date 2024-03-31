using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlInGameScreen : Screens
{
    [SerializeField] private Button startGame;

    private void Awake()
    {
        startGame.onClick.AddListener(StartGame);
    }

    void StartGame()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;

        InterfaceManager.Instance.inGame = true;
    }
}
