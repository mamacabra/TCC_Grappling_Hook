using UnityEngine;
using UnityEngine.SceneManagement;
using System;

namespace SceneSelect
{
    public class ScenesManager : MonoBehaviour
    {
        private static ScenesManager instance;
        public static ScenesManager Instance => instance ? instance : FindObjectOfType<ScenesManager>();
        public ScenesData scenesData;
        public static AsyncOperation onSceneLoadOperation;
        public static AsyncOperation onSceneUnloadOperation;
        private int currentSceneIndex;

        public void LoadRandomScene() {
            
            if (onSceneLoadOperation != null)
            {
                UnloadCurrentScene();
            }
            
            currentSceneIndex = scenesData.GetRandomSceneIndex();
            onSceneLoadOperation = SceneManager.LoadSceneAsync(currentSceneIndex, LoadSceneMode.Additive);
        }

        public void UnloadCurrentScene() {
            onSceneUnloadOperation = SceneManager.UnloadSceneAsync(currentSceneIndex, UnloadSceneOptions.None);
        }
    }
}
