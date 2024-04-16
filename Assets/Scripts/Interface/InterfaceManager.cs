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
     FinalFeedbackGame = 7
     
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
     
     [SerializeField]public List<PlayersManager.PlayerConfigurationData> playerScores = new List<PlayersManager.PlayerConfigurationData>();

     public event Action OnAddPlayersToList;
     private void OnEnable()
     {
          PlayersManager.Instance.OnPlayerConfigAdd += AddPlayersToList;
          PlayersManager.Instance.OnPlayerConfigRemove += RemovePlayers;
          PlayersManager.Instance.OnPlayerDeath += OnPlayerDeath;

          if (playerScores.Count > 0)
               playerScores.Clear();
     }

     private void OnDisable()
     {
          if (!PlayersManager.Instance) return;
          PlayersManager.Instance.OnPlayerConfigAdd -= AddPlayersToList;
          PlayersManager.Instance.OnPlayerConfigRemove -= RemovePlayers;
          PlayersManager.Instance.OnPlayerDeath -= OnPlayerDeath;
     }

     private void Start()
     {
          if(gameWithScreens) ShowScreen();
     }

     private void Update()
     {
          if (Keyboard.current.escapeKey.wasPressedThisFrame)
          {
               if (inGame)
               {
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
     
     public void AddPlayersToList(PlayersManager.PlayerConfigurationData playerData)
     {
          /*GameObject p = Instantiate(playerScoreGameObject, playerScoreObj);
          PlayerScore pS = p.GetComponent<PlayerScore>();
          pS.data.id = playerData.id;
          pS.data.score = playerData.score;*/
          playerScores.Add(playerData);
     }
     public void RemovePlayers(PlayersManager.PlayerConfigurationData playerData)
     {
          playerScores.Remove(playerData);
     }

     public void OnPlayerDeath(int id)
     {
          PlayersManager.PlayerConfigurationData pS = playerScores[GetPlayerById(id)];
          pS.ChangeScore(10);
          playerScores[GetPlayerById(id)] = pS;
          //Debug.Log(pS.score);
          
          OnAddPlayersToList?.Invoke();
     }

     int GetPlayerById(int id){
          for (int i = 0; i < playerScores.Count; i++)
          {
               if(playerScores[i].id == id) return i;    
          }
          return -1;
     }
     
}
