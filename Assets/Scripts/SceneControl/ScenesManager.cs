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
        private int currentSceneIndex = -1;

        public void LoadRandomScene() {

            if (currentSceneIndex != -1)
                UnloadCurrentScene();

            Cursor.visible = false;
            currentSceneIndex = scenesData.GetNextScene();
            onSceneLoadOperation = SceneManager.LoadSceneAsync(currentSceneIndex, LoadSceneMode.Additive);
        }

        public void UnloadCurrentScene() {

            onSceneUnloadOperation = SceneManager.UnloadSceneAsync(currentSceneIndex, UnloadSceneOptions.None);
            currentSceneIndex = -1;
        }

        public void ReloadMainScene() {
            SceneManager.LoadSceneAsync(scenesData.mainSceneData.SceneIndex, LoadSceneMode.Single);
        }
    }
}
