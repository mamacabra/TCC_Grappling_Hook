using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Input = UnityEngine.Windows.Input;

public enum ScreensName
{
     Initial_Screen=0,
     Options_Screen=1,
     Credits_Screen=2,
     CharacterChoice_Screen=3,
     Controls_InGame_Screen=4,
     Pause_InGame_Screen=5,
     FeedbackGame_Screen = 6,
     FinalFeedbackGame = 7,
     Features_Screen = 8,
     Hud = 9

}
public class InterfaceManager : MonoBehaviour
{
     #region Singleton

     private static InterfaceManager instance;
     public static InterfaceManager Instance => instance ? instance : FindObjectOfType<InterfaceManager>();

     #endregion
     
     [Header("Screens")]
     [SerializeField] private List<GameObject> screensObj;

     private int screensIndex = 0;

     public bool gameWithScreens;
     [HideInInspector] public bool inGame = false;
     [HideInInspector] public bool pause = false;
     [HideInInspector] public bool startNewGame = false;
     [HideInInspector] public bool isOnCount = false;
     [HideInInspector] public bool isOnFeedback = false;
     public event Action OnHideButton;
     public event Action OnShowScoreInFeedbackScreen;
     public event Action OnStartCount;
     private void Start()
     {
          if(gameWithScreens) ShowScreen();
     }

     public void GameOver()
     {
          if (pause)
          {
               HideSpecificScreen(ScreensName.Pause_InGame_Screen);
               ShowSpecificScreen(ScreensName.Hud);
               pause = false;
               Time.timeScale = 1;
          }
     }

     public void StartCount()
     {
          startNewGame = true;
          OnStartCount?.Invoke();
     }
     private void Update()
     {
          bool startPressed = false;
          foreach (var gamepad in Gamepad.all) {
               if (gamepad.startButton.wasPressedThisFrame) {
                    startPressed = true;
                    break;
               }
          }
          foreach (var device in InputSystem.devices) {
               if (device is Keyboard keyboard && keyboard.escapeKey.wasPressedThisFrame) {
                    startPressed = true;
                    break;
               }
          }
          if (startPressed)
          {
               if (inGame)
               {
                    if(isOnCount || isOnFeedback || PlayersManager.Instance.GameOver)return;
                    if (!pause)
                    {
                         ShowSpecificScreen(ScreensName.Pause_InGame_Screen);
                         pause = true;
                    }
                    else
                    {
                         HideSpecificScreen(ScreensName.Pause_InGame_Screen);
                         pause = false;
                    }
               }
          }
     }

     public void ShowScreen()
     {
          foreach (var o in screensObj)
               o.SetActive(false);
          
          screensObj[screensIndex].SetActive(true);

          if (screensIndex < screensObj.Count)
               screensIndex++;
     }

     public void ShowSpecificScreen(ScreensName screenName)
     {
          foreach (var o in screensObj)
               o.SetActive(false);
          
          screensObj[(int)screenName].SetActive(true);
          
          screensIndex = (int)screenName;
     }
     
     public void HideSpecificScreen(ScreensName screenName)
     {
          screensObj[(int)screenName].SetActive(false);
          //Time.timeScale = 1;
     }

     public void QuitGame()
     {
          Application.Quit();
     }
     
     public void OnCallFeedbackGame(bool gameOver)
     {
          StartCoroutine(WaitToShowFeedback());
          IEnumerator WaitToShowFeedback()
          {
               yield return new WaitForSeconds(0.25f);
               yield return new WaitUntil(() => CameraManager.Instance.OnEndFeedback); 
               ShowSpecificScreen(ScreensName.FeedbackGame_Screen);
               HideSpecificScreen(ScreensName.Hud);
               if(!gameOver) yield break;
               OnHideButton?.Invoke();
               StartCoroutine(WaitToCheckWinnerGame());
          }
     }
     IEnumerator WaitToCheckWinnerGame()
     {
          yield return new WaitForSeconds(4);
          ShowSpecificScreen(ScreensName.FinalFeedbackGame);
     }

     public void ShowScoreInFeedbackScreen()
     {
          OnShowScoreInFeedbackScreen?.Invoke();
     }
     
}