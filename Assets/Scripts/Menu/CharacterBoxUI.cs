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
        int dir = 0;
        if (context.control.device.name == "Gamepad") {
            if (context.action.WasPressedThisFrame()){
                dir = Mathf.RoundToInt(context.ReadValue<Vector2>().x);
            }
        } else {
            if (context.action.WasPerformedThisFrame()) {
                dir = Mathf.RoundToInt(context.ReadValue<Vector2>().x);
            }
        }
        ChangeColor(dir);
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
        characterImage.color = GetColor(playerConfig.characterColor);
    }

    public Color GetColor(PlayersManager.CharacterColor characterColor) {
        Color color = Color.white;
        switch (characterColor) {
            case PlayersManager.CharacterColor.White: break;
            case PlayersManager.CharacterColor.Red: color = Color.red; break;
            case PlayersManager.CharacterColor.Green: color = Color.green; break;
            case PlayersManager.CharacterColor.Blue: color = Color.blue; break;
            case PlayersManager.CharacterColor.Yellow: color = Color.yellow; break;
            case PlayersManager.CharacterColor.Pink: color = Color.magenta; break;
            default: break;
        }
        return color;
    }
}
