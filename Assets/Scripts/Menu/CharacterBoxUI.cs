using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
using Unity.VisualScripting;

public class CharacterBoxUI : MonoBehaviour
{
    [SerializeField] private Image characterImage;
    [SerializeField] private TextMeshProUGUI characterName;
    [SerializeField] PlayersManager.CharacterColor characterColor;
    public void OnMove(InputAction.CallbackContext context)
    {
        if(context.performed){
            int dir = (int)context.ReadValue<Vector2>().x;
            ChangeColor(dir);
        }
    }

    public void ChangeColor(int dir){
        int value = ((int)characterColor + dir);
        if(value < 0) value =  (int)PlayersManager.CharacterColor.Count - 1;
        if(value > (int)PlayersManager.CharacterColor.Count - 1) value = 0;
        characterColor = (PlayersManager.CharacterColor)value;
        characterImage.color = GetColor(characterColor);
    }

    public Color GetColor(PlayersManager.CharacterColor characterColor){
        Color color = Color.white;
        switch (characterColor)
        {
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
