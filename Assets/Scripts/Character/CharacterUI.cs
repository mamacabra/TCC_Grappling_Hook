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
        [SerializeField] private Transform characterCanvas;
        [SerializeField] private Transform characterCanvasPanel;

        [Header("CharacterUI Texts")]
        [SerializeField] private Text characterStateText;
        [SerializeField] private Text forceText;
        [SerializeField] private Text powerUpText;

        private void Start()
        {
            characterUI = transform.Find("CharacterUI");
            characterCanvas = characterUI?.Find("Canvas");
            characterCanvasPanel = characterUI?.Find("Canvas/Panel");

            // forceText = characterUI?.Find("Canvas/ForceText").GetComponent<Text>();
            // characterStateText = characterUI?.Find("Canvas/CharacterStateText").GetComponent<Text>();
            powerUpText = characterUI?.Find("Canvas/PowerUpsText").GetComponent<Text>();

            HiddenCharacterUI();
        }

        private void FixedUpdate()
        {
            if (!characterUI) return;

            var dir = new Vector3(0, 70, -70);
            characterUI?.LookAt(dir);
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
            if (powerUps.Count > 0)
            {
                ShowCharacterUI();
                var texts = powerUps.Aggregate("", (current, status) => current + "\n" + status);
                powerUpText.text = texts.Replace("PowerUp", "").Trim();
            }
            else
            {
                HiddenCharacterUI();
                powerUpText.text = "";
            }
        }

        private void ShowCharacterUI()
        {
            characterUI?.gameObject.SetActive(true);
            characterCanvas?.gameObject.SetActive(true);
            characterCanvasPanel?.gameObject.SetActive(true);
        }

        public void HiddenCharacterUI()
        {
            characterUI?.gameObject.SetActive(false);
            characterCanvas?.gameObject.SetActive(false);
            characterCanvasPanel?.gameObject.SetActive(false);
        }
    }
}
