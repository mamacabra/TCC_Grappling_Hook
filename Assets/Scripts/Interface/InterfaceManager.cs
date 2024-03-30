using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ScreensName
{
     Initial_Screen,
     Options_Screen,
     Credits_Screen,
     CharacterChoice_Screen,
     Controls_InGame_Screen,
     Pause_InGame_Screen,
     
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
          
          GameObject s = screensObj.Find(o => o.name == screenName.ToString());
          s.SetActive(true);
          
          screensIndex = screensObj.IndexOf(s);
     }

     public void QuitGame()
     {
          Application.Quit();
     }
}
