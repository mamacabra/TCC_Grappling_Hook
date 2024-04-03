using System;
using System.Collections.Generic;
using System.IO;
using Character;
using UnityEngine;
using UnityEngine.InputSystem;
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
        public int[] inputDevices;
        public CharacterColor characterColor;
        public ECharacterType characterModel;
    }

    // Singleton
    public static PlayersManager Instance {get; private set;}

    #region PlayerPrefabs
    [Header("Player Prefab Type")]
    public GameObject playerPrefab;
    public GameObject playerUIPrefab;
    #endregion

    #region Refferences
    [Header("Refferences")]
    public CharacterChoiseScreen characterChoice;
    public PrototypeCameraMoviment cameraMovement;
    PlayerInputManager playerInputManager;
    #endregion

    #region Data
    private bool sharingKeyboard = false;
    private int amountOfPlayersReady = 0;
    private bool[] freeId = {true, true, true, true, true, true};
    private List<PlayerConfigurationData> playersConfigs = new List<PlayerConfigurationData>();
    private string path;
    private GameObject[] playersGameObjects;
    #endregion

    // Input
    private Menus_Input actions;

    private void Awake() {
        if (Instance != null) {
            Destroy(Instance.gameObject);
        }
        Instance = this;

        playerInputManager = GetComponent<PlayerInputManager>();
        actions = new Menus_Input();
        actions.Navigation.Join.performed += OnJoin;
        playersGameObjects = new GameObject[playerInputManager.maxPlayerCount];
        path = Application.persistentDataPath + "/playersInputs.json";
    }
    
    public void InitCharacterSelection() {
        playerInputManager.playerPrefab = playerUIPrefab;
        playerInputManager.EnableJoining();
        actions.Navigation.Enable();
    }

    public void InitGame(bool loadScene = true) {
        RemoveAllPlayers();
        playersConfigs.Clear();
        LoadPlayersConfigs(); // Reset playersConfig in characterSelect screen.
        playerInputManager.playerPrefab = playerPrefab;
        Disable();
        if (playersConfigs.Count > 0) {
            if (loadScene) {
                SceneManager.LoadSceneAsync("Level.Japan.01", LoadSceneMode.Additive).completed += delegate {
                    foreach (var item in playersConfigs) {
                        // Set player material
                        PlayerInput playerInput = playerInputManager.JoinPlayer(item.id, controlScheme: item.controlScheme, pairWithDevices: GetDevicesFromString(item.inputDevices));
                        if (playerInput.TryGetComponent(out CharacterMesh characterMesh)) {
                            characterMesh.SetMesh(item.characterModel);
                            characterMesh.SetColor(item.characterColor);
                        }
                    }
                    playerInputManager.DisableJoining();
                };
            }
            else {
                foreach (var item in playersConfigs) {
                    // Set player material
                    PlayerInput playerInput = playerInputManager.JoinPlayer(item.id, controlScheme: item.controlScheme, pairWithDevices: GetDevicesFromString(item.inputDevices));
                    if (playerInput.TryGetComponent(out CharacterMesh characterMesh)) {
                        characterMesh.SetMesh(ECharacterType.Sushi);
                        characterMesh.SetColor(item.characterColor);
                    }
                }
                playerInputManager.DisableJoining();
            }
        }
    }

    public void Disable() {
        actions.Navigation.Disable();
    }

    private void OnJoin(InputAction.CallbackContext context) {// Join Input Pressed
        // Check if the action was triggered by a control
        if (context.control != null) {
            // Get the input device
            InputDevice device = context.control.device;

            // Get the control scheme
            int bindingIndex = context.action.GetBindingIndexForControl(context.control);
            string controlScheme = context.action.bindings[bindingIndex].groups;

            // Get free id
            int id = GetFreeId();
            if (id == -1) return; // not has free id return

            // Check if already has p2 on keyboard
            // Join a new player
            if (controlScheme == "KeyboardP2") {
                if (sharingKeyboard) return; 
                else sharingKeyboard = true;
                playerInputManager.JoinPlayer(id, controlScheme: controlScheme, pairWithDevices: device);
            } 
            else {
                if (sharingKeyboard &&  controlScheme == "Keyboard&Mouse") playerInputManager.JoinPlayer(id, controlScheme: controlScheme, pairWithDevices: device);
                else playerInputManager.JoinPlayerFromActionIfNotAlreadyJoined(context);
            }
        }
    }

    public void OnPlayerJoinedEvent(PlayerInput _playerInput) {

        bool inGame = InterfaceManager.Instance ? InterfaceManager.Instance.inGame : true;
        if (!inGame) {// Is in character selection screen.
            _playerInput.transform.SetParent(characterChoice.charactersGroup);
            freeId[_playerInput.playerIndex] = false;
            if (_playerInput.TryGetComponent<CharacterBoxUI>(out CharacterBoxUI characterBoxUI)) {
                characterBoxUI.playerConfig.id = _playerInput.playerIndex;
                characterBoxUI.playerConfig.controlScheme = _playerInput.currentControlScheme;
                characterBoxUI.playerConfig.inputDevices = GetStringFromDevices(_playerInput.devices.ToArray());
            }
        } else {
            if (!cameraMovement) cameraMovement = FindAnyObjectByType<PrototypeCameraMoviment>();
            if (cameraMovement) cameraMovement.RecivePlayers(_playerInput.transform);
            else {
                ResourcesPrefabs resourcesPrefabs = Resources.Load<ResourcesPrefabs>("ResourcesPrefabs");
                PrototypeCameraMoviment prototypeCameraMoviment = resourcesPrefabs.prefabs[(int)ResourcesPrefabs.PrefabType.Camera].GetComponent<PrototypeCameraMoviment>();
                cameraMovement = Instantiate(prototypeCameraMoviment);
                cameraMovement.RecivePlayers(_playerInput.transform);
            }
            // Get Spawns positions
        }
        playersGameObjects[_playerInput.playerIndex] = _playerInput.gameObject;
    }

    public void OnPlayerLeftEvent(PlayerInput _playerInput) {
        if (_playerInput.currentControlScheme == "KeyboardP2") sharingKeyboard = false;
        freeId[_playerInput.playerIndex] = true;
    }

    public void SetPlayerStatus(bool isReady) {
        if (isReady) {
            amountOfPlayersReady++;
        } else {
            amountOfPlayersReady--;
        } if (amountOfPlayersReady == playerInputManager.playerCount) {
            // Do stuff
            SavePlayersConfigs();
        }
    }

    public void AddNewPlayerConfig(PlayerConfigurationData playerConfiguration) {
        playersConfigs.Add(playerConfiguration);
    }

    public void RemovePlayerConfig(PlayerConfigurationData playerConfiguration) {
        playersConfigs.Remove(playerConfiguration);
    }
    public void ClearPlayersConfig() {
        playersConfigs.Clear();
        for (int i = 0; i < playersGameObjects.Length; i++) {
            if (playersGameObjects[i]) {
                Destroy(playersGameObjects[i]);
            }
        }
    }

    public void SavePlayersConfigs() {
        PlayersInputData playersInputData = new PlayersInputData();
        playersInputData.playersConfigs = this.playersConfigs.ToArray();
        string json = JsonUtility.ToJson(playersInputData);
        File.WriteAllText(path, json);
    }

    private void LoadPlayersConfigs() {
        string json = File.ReadAllText(path);
        if (string.IsNullOrEmpty(json)) {
            Debug.LogWarning("USING DEFAULT PLAYER INPUTS!!!, probaly not started the game from menu");
            var devices = new int[] {Keyboard.current.deviceId, Mouse.current.deviceId };
            var p1Config = new PlayerConfigurationData {id = 0, characterColor = CharacterColor.White, controlScheme = "Keyboard&Mouse", inputDevices = devices};
            var p2Config = new PlayerConfigurationData {id = 1, characterColor = CharacterColor.Blue, controlScheme = "KeyboardP2", inputDevices = new int[] {devices[0]} };
            playersConfigs = new List<PlayerConfigurationData> {p1Config, p2Config};
        }
        PlayersInputData playersInputData = JsonUtility.FromJson<PlayersInputData>(json);
        for (int i = 0; i < playersInputData.playersConfigs.Length; i++) {
            this.playersConfigs.Add(playersInputData.playersConfigs[i]);
        }
    }

    private InputDevice[] GetDevicesFromString(int[] devices) {
        List<InputDevice> result = new List<InputDevice>();
        InputDevice[] allDevices = InputSystem.devices.ToArray();
        for (int i = 0; i < allDevices.Length; i++) {
            for (int j = 0; j < devices.Length; j++) {
                if (allDevices[i].deviceId == devices[j]) {
                    result.Add(allDevices[i]);
                }
            }
        }
        return result.ToArray();
    }
    private int[] GetStringFromDevices(InputDevice[] devices) {
        int[] result = new int[devices.Length];
        for (int i = 0, j = 0; i < devices.Length; i++, j++) {
            result[i] = devices[j].deviceId;
        }
        return result;
    }
    private int GetFreeId() {
        int result = -1;
        for (int i = 0; i < freeId.Length; i++) {
            if(freeId[i]) {
                result = i;
                break;
            }
        }
        return result;
    }
    public void RemoveAllPlayers() {
        for (int i = 0; i < playersGameObjects.Length; i++) {
            if (playersGameObjects[i]) {
                Destroy(playersGameObjects[i]);
            }
        }
    }

    public static Color GetColor(PlayersManager.CharacterColor characterColor) {
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
[Serializable]
public class PlayersInputData {
    public PlayersManager.PlayerConfigurationData[] playersConfigs;
}

