using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[Serializable]
public struct ButtonToScreen
{
    public Button button;
    public ScreensName goToScreen;
}
public abstract class Screens : MonoBehaviour
{
    public void OnEnable()
    {
        Initialize();
    }

    private void OnDisable()
    {
        Close();
    }

    public virtual void Initialize()
    { 
       
    }

    public virtual void Close()
    {
        
    }

    public virtual void NextScreen()
    {
        InterfaceManager.Instance.ShowScreen();
    }
    
    public virtual void GoToScreen(ScreensName screenName)
    {
        InterfaceManager.Instance.ShowSpecificScreen(screenName);
    }
}