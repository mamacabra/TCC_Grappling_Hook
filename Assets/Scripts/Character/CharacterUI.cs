using System;
using Character.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Character
{
    [ExecuteInEditMode]
    public class CharacterUI : ACharacterMonoBehaviour
    {
        [SerializeField] private GameObject characterUI;

        [Header("Character UI")]
        [SerializeField] private Text statusText;

        [Header("Grappling Hook UI")]
        [SerializeField] private Text forceText;

        private void Start()
        {
            characterUI = GameObject.Find("CharacterUI");
            statusText = GameObject.Find("Character - StateText").GetComponent<Text>();
            forceText = GameObject.Find("GrapplingHook - ForceText").GetComponent<Text>();
        }

        private void Update()
        {
            characterUI?.transform.LookAt(Camera.main.transform.position);
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
