using System;
using UnityEngine.InputSystem;
namespace LocalMultiplayer.Data {
    [Serializable]
    public struct PlayerConfigurationData {
        public string controlScheme;
        public InputDevice[] inputDevices;
        public string[] inputDevicesNames;
        public CharacterColor characterColor;
        public ECharacterType characterModel;
        public int score;
        public int id;
        public int characterIndexCharacterChoice;
        public void ChangeScore(int s) {
            score += s;
        }
    }
}
