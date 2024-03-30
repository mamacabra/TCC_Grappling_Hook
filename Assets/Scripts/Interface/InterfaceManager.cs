using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ScreensName
{
     Initial_Screen=0,
     Options_Screen=1,
     Credits_Screen=2,
     CharacterChoice_Screen=3,
     Controls_InGame_Screen=4,
     Pause_InGame_Screen=5,
     
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

     private void Start()
     {
          if(gameWithScreens) ShowScreen();
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

     public void QuitGame()
     {
          Application.Quit();
     }
}
