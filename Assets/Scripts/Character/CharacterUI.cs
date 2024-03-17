using UnityEngine;
using UnityEngine.UI;

namespace Character
{
    public class CharacterUI : MonoBehaviour
    {
        [Header("Character UI")]
        public Text statusText;

        [Header("Grappling Hook UI")]
        public Text forceText;

        public void UpdateStatusUI(string status)
        {
            if (statusText) statusText.text = "Sts: " + status;
        }

        public void UpdateForceUI(int force)
        {
            if (forceText) forceText.text = "GH: " + force;
        }
    }
}
