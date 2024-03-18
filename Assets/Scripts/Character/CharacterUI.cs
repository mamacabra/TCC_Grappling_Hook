using System;
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

        private void Start()
        {
            statusText = GameObject.Find("Character - StateText").GetComponent<Text>();
            forceText = GameObject.Find("GrapplingHook - ForceText").GetComponent<Text>();
        }

        public void UpdateStatusUI(string status)
        {
            if (statusText) statusText.text = status;
        }

        public void UpdateForceUI(int force)
        {
            if (forceText) forceText.text = "GH: " + force;
        }
    }
}
