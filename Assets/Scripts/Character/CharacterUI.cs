using System.Collections.Generic;
using System.Linq;
using Character.Utils;
using Const;
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

        [Header("Power Ups")]
        [SerializeField] private Text powerUpText;

        private void Start()
        {
            characterUI = transform.Find("CharacterUI");
            characterUI.gameObject.SetActive(true);

            FindTexts();
        }

        private void FixedUpdate()
        {
            if (characterUI)
            {
                var dir = new Vector3(0, 70, -70);
                characterUI?.LookAt(dir);
            }
        }

        private void FindTexts()
        {
            // forceText = characterUI?.Find("Canvas/ForceText").GetComponent<Text>();
            // characterStateText = characterUI?.Find("Canvas/CharacterStateText").GetComponent<Text>();
            powerUpText = characterUI?.Find("Canvas/PowerUpsText").GetComponent<Text>();
        }

        public void UpdateCharacterStateUI(string status)
        {
            if (characterStateText) characterStateText.text = status;
        }

        public void UpdateForceUI(int force)
        {
            if (forceText) forceText.text = "GH: " + force;
        }

        public void UpdatePowerUpsUI(List<PowerUpVariants> powerUps)
        {
            if (!powerUpText) return;

            var texts = powerUps.Aggregate("", (current, status) => current + "\n" + status);
            powerUpText.text = texts.Replace("PowerUp", "").Trim();
        }
    }
}
