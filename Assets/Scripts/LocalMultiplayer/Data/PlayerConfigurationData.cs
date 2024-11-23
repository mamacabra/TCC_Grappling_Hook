using System;
namespace LocalMultiplayer.Data {
    [Serializable]
    public struct PlayerConfigurationData {
        public string controlScheme;
        public int[] inputDevices;
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
