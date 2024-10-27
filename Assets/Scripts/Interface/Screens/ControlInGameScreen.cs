using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ControlInGameScreen : Screens
{
    [SerializeField] private ButtonToScreen startGame;

    private void Awake()
    {
        startGame.button.onClick.AddListener(StartGame);
    }
    
    public override void Initialize()
    {
        startGame.button.gameObject.SetActive(false);

        StartCoroutine(WaitToShowButton());
        IEnumerator WaitToShowButton()
        {
            yield return new WaitForSeconds(2);
            startGame.button.gameObject.SetActive(true);
            EventSystem.current.SetSelectedGameObject(startGame.button.gameObject);
        }
    }

    void StartGame()
    {
        
        GoToScreen(startGame.goToScreen);
        gameObject.SetActive(false);
        Time.timeScale = 1;
        
        if (PlayersManager.Instance) PlayersManager.Instance.InitGame();
    }
}
