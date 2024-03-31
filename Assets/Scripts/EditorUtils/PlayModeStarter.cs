#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class PlayModeStarter{
    static PlayModeStarter(){
        EditorApplication.playModeStateChanged += OnPlayModeChanged;
    }

    private static void OnPlayModeChanged(PlayModeStateChange state)
    {
        if(state == PlayModeStateChange.EnteredPlayMode){
            GameObject gameObject = new GameObject("----TestingSceneObject----");
            gameObject.AddComponent<TestSceneObject>();
        }
    }
}

#endif