using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FeaturesScreen : Screens
{
    [Header("Buttons")]
    [SerializeField] private List<ButtonToScreen> buttons;
    
    private void Awake()
    {
        foreach (var b in buttons)
            b.button.onClick.AddListener(delegate { GoToScreen(b.goToScreen); });
    }
    public override void Initialize()
    {
        EventSystem.current.SetSelectedGameObject(buttons[0].button.gameObject);
        PlayersManager.Instance.ClearPlayersConfig();
    }

    public override void GoToScreen(ScreensName screensName)
    {
        base.GoToScreen(screensName);
    }
}
