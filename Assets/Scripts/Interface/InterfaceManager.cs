using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Input = UnityEngine.Windows.Input;
using LocalMultiplayer;

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

    /* [SerializeField] private List<Screens> screensList;
     private Screens currentScreen;*/

     public NotificationManager notificationManager;

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
     public event Action OnRestartGame;
     public event Action OnResetAllSettings;
     private void Start()
     {
          foreach (var p in InputSystem.devices)
          {
               InputSystem.EnableDevice(p);
          }
          if(gameWithScreens) ShowScreen();
     }

     public void GameOver()
     {
          if (pause)
          {
               HideSpecificScreen(ScreensName.Pause_InGame_Screen);
               ShowSpecificScreen(ScreensName.Hud);
          }
     }

     public void RestartGame()
     {
          OnRestartGame?.Invoke();
     }
     public void StartCount()
     {
          startNewGame = true;
          OnStartCount?.Invoke();
     }

     public InputDevice currentDevice;
     private void Update()
     {
          bool startPressed = false;
          foreach (var gamepad in Gamepad.all) {
               if (gamepad.startButton.wasPressedThisFrame)
               {
                    if(!pause) currentDevice = gamepad;
                    else { if (gamepad != currentDevice) return; }
                    startPressed = true;
                    break;
               }
          }
          foreach (var device in InputSystem.devices) {
               if (device is Keyboard keyboard && keyboard.escapeKey.wasPressedThisFrame)
               {
                    if(!pause) currentDevice = keyboard;
                    else { if (currentDevice != null) return; }
                    startPressed = true;
                    break;
               }
          }
          // Impede input de pause de dispositivo que não está em jogo
          bool deviceIsAPlayer = false;
          foreach (var p in PlayersManager.Instance.ReturnPlayersList()){
               if (currentDevice == p.inputDevices[0]){
                    deviceIsAPlayer = true;
                    break;
               }
          }
          if(!deviceIsAPlayer) return;
          if (startPressed)
          {
               if (inGame)
               {
                    if(isOnCount || isOnFeedback || PlayersManager.Instance.GameOver)return;
                    if (!pause)
                    {
                         pause = true;

                         foreach (var p in InputSystem.devices)
                         {
                              if(p != currentDevice)
                                   InputSystem.DisableDevice(p);
                         }
                         
                         ShowSpecificScreen(ScreensName.Pause_InGame_Screen);
                    }
                    else
                    {
                         HideSpecificScreen(ScreensName.Pause_InGame_Screen);
                         ShowSpecificScreen(ScreensName.Hud);
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

     public void ReturnCurrentScreen() {
          var currentScreen = (ScreensName)screensIndex;

          switch ((ScreensName)screensIndex)
          {
               case ScreensName.Initial_Screen:break;
               case ScreensName.Options_Screen or ScreensName.Credits_Screen or ScreensName.Features_Screen:
                    ShowSpecificScreen(ScreensName.Initial_Screen);
                break;
                    
               case ScreensName.CharacterChoice_Screen:
                    if (PlayersManager.Instance.playerInputManager.playerCount == 0)
                         PlayersManager.Instance.characterChoice.GoToScreen(ScreensName.Initial_Screen);
                    break;
               
               case ScreensName.Controls_InGame_Screen: break;
               case ScreensName.Pause_InGame_Screen: break;
               case ScreensName.FeedbackGame_Screen: break;
               case ScreensName.FinalFeedbackGame: break;

               default:
                    //GoToScreen(ScreensName.Initial_Screen);
                    break;
          }
     }

     public void ResetOptions()
     {
          if (GetCurrentScreenIndex == (int)ScreensName.Options_Screen)
          {
               OnResetAllSettings?.Invoke();
          }
     }
     public void ShowScoreInFeedbackScreen()
     {
          OnShowScoreInFeedbackScreen?.Invoke();
     }

     public int GetCurrentScreenIndex => screensIndex;
     
}