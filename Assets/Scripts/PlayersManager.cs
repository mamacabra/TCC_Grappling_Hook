using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerInputManager))]
public class PlayersManager : MonoBehaviour
{
    public enum CharacterColor {
        White = 0,
        Red = 1,
        Green = 2,
        Blue = 3,
        Yellow = 4,
        Pink = 5,
        Count = 6
    }
    [Serializable]
    public struct PlayerConfigurationData {
        public int id;
        public string controlScheme;
        public InputDevice[] inputDevices;
        public CharacterColor characterColor;
    }
    public static PlayersManager Instance {get; private set;}

    public GameObject playerPrefab;
    public GameObject playerUIPrefab;
    PlayerInputManager playerInputManager;
    [HideInInspector] public CharacterSelectUI characterSelect;
    bool sharingKeyboard = false;
    int amountOfPlayersReady = 0;
    List<PlayerConfigurationData> playersConfigs = new List<PlayerConfigurationData>();

    private void Awake() {
        if (Instance != null) {
            this.playersConfigs = Instance.playersConfigs;
            Destroy(Instance.gameObject);
        }
        Instance = this;
        DontDestroyOnLoad(Instance);

        playerInputManager = GetComponent<PlayerInputManager>();
    }

    private void Start() {
        if(playersConfigs.Count > 0) playerInputManager.playerPrefab = playerPrefab;
        else playerInputManager.playerPrefab = playerUIPrefab;
        foreach (var item in playersConfigs) {
            playerInputManager.JoinPlayer(item.id, controlScheme: item.controlScheme, pairWithDevices: item.inputDevices);
        }
    }

    public void OnPlayerJoinedEvent(PlayerInput _playerInput) {
        if (_playerInput.currentControlScheme == "KeyboardP2") sharingKeyboard = true;
        if (characterSelect) {
            _playerInput.transform.SetParent(characterSelect.charactersGroup);
            if (_playerInput.TryGetComponent<CharacterBoxUI>(out CharacterBoxUI characterBoxUI)) {
                characterBoxUI.playerConfig.id = _playerInput.playerIndex;
                characterBoxUI.playerConfig.controlScheme = _playerInput.currentControlScheme;
                characterBoxUI.playerConfig.inputDevices = _playerInput.devices.ToArray();
            }
        }else{
            // Get Spawns positions
        }
    }

    public void SetPlayerStatus(bool isReady) {
        if (isReady) {
            amountOfPlayersReady++;
        }
        else {
            amountOfPlayersReady--;
        }
        if (amountOfPlayersReady == playerInputManager.playerCount) {
            //Do stuff
            SceneManager.LoadScene("SampleGameScene");// TODO: Change this to make scene addictive
        }
    }

    public void AddNewPlayerConfig (PlayerConfigurationData playerConfiguration) {
        playersConfigs.Add(playerConfiguration);
    }
    public void RemovePLayerConfig (PlayerConfigurationData playerConfiguration) {
        playersConfigs.Remove(playerConfiguration);
    }

    private void Update() {
        if (Keyboard.current.jKey.wasPressedThisFrame && !sharingKeyboard && characterSelect) {
            playerInputManager.JoinPlayer(playerInputManager.playerCount, controlScheme: "KeyboardP2", pairWithDevice: Keyboard.current);
        }
    }
}
