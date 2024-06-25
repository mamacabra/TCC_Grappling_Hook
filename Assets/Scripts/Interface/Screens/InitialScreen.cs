using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InitialScreen : Screens
{
    [Header("Buttons")]
    [SerializeField] private List<ButtonToScreen> buttons;
    [SerializeField] private Button quit_Button;

    private void Awake()
    {
        foreach (var b in buttons)
            b.button.onClick.AddListener(delegate { GoToScreen(b.goToScreen); });
        quit_Button.onClick.AddListener(QuitGame);
    }
    public override void Initialize()
    {
        PlayersManager.Instance.ClearPlayersConfig(charactersFromGame: true);
        PlayersManager.Instance.SavePlayersConfigs();
        EventSystem.current.SetSelectedGameObject(buttons[0].button.gameObject);
    }

    public override void GoToScreen(ScreensName screensName)
    {
        base.GoToScreen(screensName);
    }

    public void QuitGame()
    {
        InterfaceManager.Instance.QuitGame();
    }
}
