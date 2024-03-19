using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInputManager))]
public class PlayersManager : MonoBehaviour
{
    public enum CharacterColor{
        White = 0,
        Red = 1,
        Green = 2,
        Blue = 3,
        Yellow = 4,
        Pink = 5,
        Count = 6
    }
    public struct PlayerConfigurationData{
        public PlayerInput playerInput;
        public CharacterColor characterColor;
    }
    public static PlayersManager Instance {get; private set;}

    PlayerInputManager playerInputManager;
    public CharacterSelectUI characterSelect;
    bool sharingKeyboard = false;
    bool playingOnKeyboard = false;

    private void Awake() {
        if(Instance != null){
            Destroy(Instance.gameObject);
        }
        Instance = this;
        DontDestroyOnLoad(Instance);

        playerInputManager = GetComponent<PlayerInputManager>();
    }

    public void OnPlayerJoinedEvent(PlayerInput playerInput) {
        if(playerInput.currentControlScheme == "Keyboard&Mouse") playingOnKeyboard = true;
        if(playerInput.currentControlScheme == "KeyboardP2") sharingKeyboard = true;
        if(characterSelect) playerInput.transform.SetParent(characterSelect.charactersGroup);
    }

    private void Update() {
        if (Keyboard.current.spaceKey.wasPressedThisFrame && !playingOnKeyboard)
            playerInputManager.JoinPlayer(playerInputManager.playerCount, controlScheme: "Keyboard&Mouse", pairWithDevices: new InputDevice[]{Keyboard.current, Mouse.current});
        else if(Keyboard.current.jKey.wasPressedThisFrame && !sharingKeyboard)
            playerInputManager.JoinPlayer(playerInputManager.playerCount, controlScheme: "KeyboardP2", pairWithDevice: Keyboard.current);
    }
}
