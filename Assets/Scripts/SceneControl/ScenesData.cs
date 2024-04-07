using UnityEngine;

namespace SceneSelect
{
    [System.Serializable]
    public struct SceneData {
        public string SceneName;
        public int SceneIndex;
    }

    [CreateAssetMenu(fileName = "ScenesData", menuName = "ScenesData", order = 0)]
    public class ScenesData : ScriptableObject {
        public SceneData[] scenesData;

        public int GetRandomSceneIndex() {
            int scene = Random.Range(0, scenesData.Length);
            return scenesData[scene].SceneIndex;
        }
    }
}
