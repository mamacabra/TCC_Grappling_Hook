using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FeedbackGame : Screens
{
    [SerializeField] private Button nextGameButton;
    private bool canclick = false;
    public override void Initialize()
    {
        StartCoroutine(WaitToCanClick());
        IEnumerator WaitToCanClick()
        {
            yield return new WaitForSeconds(1.5f);
            canclick = true;
        }
        nextGameButton.gameObject.SetActive(true);
        InterfaceManager.Instance.OnHideButton += HideButton;
        EventSystem.current.SetSelectedGameObject(nextGameButton.gameObject);
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
        if(!canclick)return;
        canclick = false;
        PlayersManager.Instance.InitGame(true);
        Close();
    }

    public override void HideButton()
    {
        nextGameButton.gameObject.SetActive(false);
    }
}