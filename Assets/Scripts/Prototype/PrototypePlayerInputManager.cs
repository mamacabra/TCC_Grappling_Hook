using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInputManager))]
public class PrototypePlayerInputManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private PrototypeCameraMoviment cameraMoviment;

    int curentPlayerAmount = 0;

    void Awake(){
    }
    void Start()
    {
        var p1 = PlayerInput.Instantiate(playerPrefab,playerIndex: 0, controlScheme: "Keyboard&Mouse", pairWithDevices: new InputDevice[]{Keyboard.current, Mouse.current});
        var p2 = PlayerInput.Instantiate(playerPrefab,playerIndex: 1, controlScheme: "KeyboardP2", pairWithDevices: new InputDevice[]{Keyboard.current});
    }

    public void OnPlayerJoinedEvent(){
        cameraMoviment.RecivePlayers(PlayerInput.all[curentPlayerAmount].transform);
        curentPlayerAmount++;
    }
}
