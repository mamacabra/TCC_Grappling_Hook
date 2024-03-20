using Character.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Character
{
    [ExecuteInEditMode]
    public class CharacterUI : ACharacterMonoBehaviour
    {
        [SerializeField] private Transform characterUI;

        [Header("Character UI")]
        [SerializeField] private Text characterStateText;

        [Header("Grappling Hook UI")]
        [SerializeField] private Text forceText;

        private void Start()
        {
            characterUI = transform.Find("CharacterUI");

            forceText = characterUI?.Find("Canvas/ForceText").GetComponent<Text>();
            characterStateText = characterUI?.Find("Canvas/CharacterStateText").GetComponent<Text>();
        }

        private void Update()
        {
            characterUI?.LookAt(Camera.main.transform.position);
        }

        public void UpdateCharacterStateUI(string status)
        {
            if (characterStateText) characterStateText.text = status;
        }

        public void UpdateForceUI(int force)
        {
            if (forceText) forceText.text = "GH: " + force;
        }
    }
}
