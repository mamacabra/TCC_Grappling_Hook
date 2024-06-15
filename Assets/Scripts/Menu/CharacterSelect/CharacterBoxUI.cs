using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class CharacterBoxUI : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Image characterImage;
    [SerializeField] private TextMeshProUGUI characterName;
    [SerializeField] private TextMeshProUGUI characterStatus;
    private bool hasConfirmed;

    public PlayersManager.PlayerConfigurationData playerConfig;

    public void ChangePlayerInput(PlayerInput p)
    {
        playerInput = p;
    }
    public void OnMove(InputAction.CallbackContext context) {
        int dir_x = 0;
        int dir_y = 0;
        if (context.action.WasPerformedThisFrame()) {
            var value = context.ReadValue<Vector2>();
            dir_x = Mathf.RoundToInt(value.x);
            dir_y = Mathf.RoundToInt(value.y);
        }
        if(dir_x != 0)
            ChangeModelImage(dir_x);
        // else
        //     ChangeColor(dir_y);
    }

    public void OnConfirm(InputAction.CallbackContext context) {
        if(PlayersManager.Instance.debug) return;
        if (hasConfirmed) return;
        if (context.action.WasPerformedThisFrame()) {
            characterStatus.text = "Pronto";
            characterStatus.color = Color.green;
            PlayersManager.Instance?.AddNewPlayerConfig(playerConfig);
            PlayersManager.Instance?.SetPlayerStatus(true);
            hasConfirmed = true;
        }
    }

    [SerializeField] private CharacterChoiseScreen choiseScreen;
    public void OnCancel(InputAction.CallbackContext context) {
        if (context.action.WasPerformedThisFrame()) {
            if (!hasConfirmed) { 
                choiseScreen.CheckGroup(transform, false);
                gameObject.SetActive(false);
                return;}
            characterStatus.text = "Escolhendo";
            characterStatus.color = Color.gray;
            PlayersManager.Instance?.RemovePlayerConfig(playerConfig);
            PlayersManager.Instance?.SetPlayerStatus(false);
            hasConfirmed = false;
        }
    }

    public void ChangeColor(int dir) {
        // Not using this anymore at this time.
        /*{ 
            if (hasConfirmed) return;
            int value = ((int)playerConfig.characterColor + dir);
            if (value < 0) value =  (int)PlayersManager.CharacterColor.Count - 1;
            if (value > (int)PlayersManager.CharacterColor.Count - 1) value = 0;
            playerConfig.characterColor = (PlayersManager.CharacterColor)value;
            characterImage.color = PlayersManager.GetColor(playerConfig.characterColor);
        }*/
    }

    public void ChangeModelImage(int dir) {
        if (hasConfirmed) return;
        int value = ((int)playerConfig.characterModel + dir);
        if (value < 0) value =  (int)ECharacterType.Count - 1;
        if (value > (int)ECharacterType.Count - 1) value = 0;
        playerConfig.characterModel = (ECharacterType)value;
        Sprite sprite = Resources.Load<ResourcesCharacters>("ResourcesCharacters").GetCharacterData((ECharacterType)value).characterSprite;
        characterImage.sprite = sprite;
    }
    
    private void OnDisable() {
        characterStatus.text = "Escolhendo";
        characterStatus.color = Color.gray;
        hasConfirmed = false;
        playerConfig = new PlayersManager.PlayerConfigurationData();
        ChangeColor((int)playerConfig.characterColor);
        ChangeModelImage((int)playerConfig.characterModel);
    }
}
