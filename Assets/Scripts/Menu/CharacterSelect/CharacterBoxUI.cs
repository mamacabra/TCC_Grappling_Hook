using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class CharacterBoxUI : MonoBehaviour
{
    [SerializeField] private Image characterImage;
    [SerializeField] private TextMeshProUGUI characterName;
    [SerializeField] private TextMeshProUGUI characterStatus;
    private bool hasConfirmed;

    public PlayersManager.PlayerConfigurationData playerConfig;

    public void OnMove(InputAction.CallbackContext context) {
        int dir_x = 0;
        int dir_y = 0;
        if (context.action.WasPerformedThisFrame()) {
            var value = context.ReadValue<Vector2>();
            dir_x = Mathf.RoundToInt(value.x);
            dir_y = Mathf.RoundToInt(value.y);
        }
        if(dir_x != 0)
            ChangeColor(dir_x);
        else
            ChangeModelImage(dir_y);
    }

    public void OnConfirm(InputAction.CallbackContext context) {
        if (hasConfirmed) return;
        if (context.action.WasPerformedThisFrame()) {
            characterStatus.text = "Pronto";
            characterStatus.color = Color.green;
            PlayersManager.Instance?.AddNewPlayerConfig(playerConfig);
            PlayersManager.Instance?.SetPlayerStatus(true);
            hasConfirmed = true;
        }
    }
    public void OnCancel(InputAction.CallbackContext context) {
        if (context.action.WasPerformedThisFrame()) {
            if (!hasConfirmed) { Destroy(this.gameObject); return;}
            characterStatus.text = "Escolhendo";
            characterStatus.color = Color.gray;
            PlayersManager.Instance?.RemovePlayerConfig(playerConfig);
            PlayersManager.Instance?.SetPlayerStatus(false);
            hasConfirmed = false;
        }
    }

    public void ChangeColor(int dir) {
        if (hasConfirmed) return;
        int value = ((int)playerConfig.characterColor + dir);
        if (value < 0) value =  (int)PlayersManager.CharacterColor.Count - 1;
        if (value > (int)PlayersManager.CharacterColor.Count - 1) value = 0;
        playerConfig.characterColor = (PlayersManager.CharacterColor)value;
        characterImage.color = PlayersManager.GetColor(playerConfig.characterColor);
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
}