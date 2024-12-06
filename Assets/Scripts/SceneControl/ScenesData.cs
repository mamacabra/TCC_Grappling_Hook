using System;
using UnityEngine;

using Random = UnityEngine.Random;

namespace SceneSelect
{
    [System.Serializable]
    public struct SceneData {
        public string SceneName;
        public int SceneIndex;
    }

    [CreateAssetMenu(fileName = "ScenesData", menuName = "ScenesData", order = 0)]
    public class ScenesData : ScriptableObject {
        public SceneData[] scenesData = new SceneData[0];
        public SceneData mainSceneData;
        private SceneData[] sortedScenes = new SceneData[0];
        private int currentRandomScene = 0;

        public int GetRandomSceneIndex() {
            int scene = Random.Range(0, scenesData.Length);
            return scenesData[scene].SceneIndex;
        }

        public void SortScenes() {
            sortedScenes = scenesData;
            int n = sortedScenes.Length;
            for (int i = 0; i < n; i++) {
                // Swap array[i] with a random element in the array
                int randomIndex = i + Random.Range(0, n - i);
                SceneData temp = sortedScenes[randomIndex];
                sortedScenes[randomIndex] = sortedScenes[i];
                sortedScenes[i] = temp;
            }
        }

        public int GetNextScene(){
            if (sortedScenes.Length != scenesData.Length) SortScenes();
            if (currentRandomScene > sortedScenes.Length - 1) {currentRandomScene = 0; SortScenes();}
            var scene = sortedScenes[currentRandomScene].SceneIndex;
            currentRandomScene++;
            return scene;
        }
    }
}
