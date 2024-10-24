using System;
using System.Collections.Generic;
using System.IO;
using Character;
using Character.Utils;
using PowerUp;
using SceneSelect;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInputManager))]
public class PlayersManager : MonoBehaviour
{
    #region StructureAndEnum
    public enum CharacterColor {
        White = 0,
        Red = 1,
        Green = 2,
        Blue = 3,
        Yellow = 4,
        Pink = 5,
    }
    [Serializable]
    public struct PlayerConfigurationData {
        public string controlScheme;
        public int[] inputDevices;
        public CharacterColor characterColor;
        public ECharacterType characterModel;
        public SCharacterData sCharacterData;
        public int score;
        public int id;
        public int characterIndexCharacterChoice;
        public void ChangeScore(int s) {
            score += s;
        }
    }
    #endregion

    private static PlayersManager instance;// Singleton
    public static PlayersManager Instance => instance ? instance : FindObjectOfType<PlayersManager>();
    private Menus_Input actions;// Input
    public  bool debug;

    #region PlayerPrefabs
    [Header("Player Prefab Type")]
    public GameObject playerPrefab;
    public GameObject playerUIPrefab;
    #endregion

    #region Refferences
    [Header("Refferences")]
    public CharacterChoiceScreen characterChoice;
    public PrototypeCameraMoviment cameraMovement;
    public PlayersSpawners playersSpawners;
    public PlayerInputManager playerInputManager;
    #endregion

    #region Data
    private bool keyboardP1 = false;
    private bool keyboardP2 = false;
    private int amountOfPlayersReady = 0;
    private bool[] freeId = {true, true, true, true, true, true};
    [SerializeField] private List<PlayerConfigurationData> playersConfigs = new List<PlayerConfigurationData>();
    [SerializeField] private List<PlayerConfigurationData> playersConfigsAUX = new List<PlayerConfigurationData>();
    private string path;
    [SerializeField] private List<GameObject> playersGameObjects;
    private bool canInitGame = false;
    public bool CanInitGame => canInitGame;

    private int winnerId = -1;

    #endregion

    #region Actions
   // public event Action<PlayerConfigurationData> OnPlayerConfigAdd;
   // public event Action<PlayerConfigurationData> OnPlayerConfigRemove;
    //public event Action<int> OnPlayerDeath;
    public event Action OnUpdateText;

    #endregion

    #region Initializers
    private void Awake() {
        playerInputManager = GetComponent<PlayerInputManager>();
        actions = new Menus_Input();
        actions.Navigation.Join.performed += OnJoin;
        actions.Navigation.Cancel.performed += OnCancel;
        playersGameObjects = new();
        path = Application.persistentDataPath + "/playersInputs.json";
    }
    public void InitCharacterSelection() {
        ClearPlayersConfig();

        canInitGame = false;
        playerInputManager.playerPrefab = playerUIPrefab;
        playerInputManager.EnableJoining();
        EnableInputActions();
    }
    public void InitGame(bool loadScene = true) {
        GameOver = false;
        InterfaceManager.Instance.isOnFeedback = false;
       
        winnerSupreme = -1;
       
        ClearPlayersConfig(charactersFromGame: true);
        LoadPlayersConfigs(); // Reset playersConfig in characterSelect screen.
        playerInputManager.playerPrefab = playerPrefab;
        DisableInputActions();
        if (playersConfigs.Count > 0) {
            if (loadScene) {
                ScenesManager.Instance.LoadRandomScene();
                ScenesManager.onSceneLoadOperation.completed += delegate {
                    int s = 0;
                    for (int i = 0; i < playersConfigs.Count; i++) {
                        if (playersConfigs[i].score > s){
                            s = playersConfigs[i].score;
                            winnerId = playersConfigs[i].id;
                        }
                    }
                    SetPlayersConfigs();
                };
            }
            else { // Test scene
                SetPlayersConfigs();
            }
        }
    }
    #endregion

    #region PlayerEvents
    private void OnJoin(InputAction.CallbackContext context) {// Join Input Pressed
        // Check if the action was triggered by a control
        if (context.control != null && context.action.WasPerformedThisFrame()) {
            // Get the input device
            InputDevice device = context.control.device;

            // Get the control scheme
            int bindingIndex = context.action.GetBindingIndexForControl(context.control);
            string controlScheme = context.action.bindings[bindingIndex].groups;

            // Get free id
            int id = GetFreeId();
            if (id == -1) return; // not has free id return

            PlayerInput player;

            if (debug) { // For add ilimitted players pressing J
                player = characterChoice.ReturnPlayerInput(true,false);
                player.SwitchCurrentControlScheme(controlScheme: controlScheme, device);
                return;
            }

            // Join a new player
            if (controlScheme == "Keyboard&Mouse") { // Keyboard P1
                if (keyboardP1) return;
                else keyboardP1 = true;
                player = characterChoice.ReturnPlayerInput(false, true);
            } else if (controlScheme == "KeyboardP2") { // Keyboard P2
                if (keyboardP2) return;
                else keyboardP2 = true;
                player = characterChoice.ReturnPlayerInput(false,false);
            } else { // Gamepad
                if (PlayerInput.FindFirstPairedToDevice(device) != null) return;
                player = characterChoice.ReturnPlayerInput(true, false);
            }
            player.SwitchCurrentControlScheme(controlScheme: controlScheme, device);
            OnPlayerJoinedEvent(player);
        }
    }
    private void OnCancel(InputAction.CallbackContext context) {
        if (context.control == null) return;

        if (context.action.WasPerformedThisFrame()) {
            if (playerInputManager.playerCount == 0) characterChoice.GoToScreen(ScreensName.Initial_Screen);
        }
    }
    public void OnPlayerJoinedEvent(PlayerInput _playerInput) {
        PlayerColorLayerManager.DefineCharacterColorLayer(_playerInput.playerIndex);
        // Trigged when player joined : set in inspector: PlayerInputManager
        bool inGame = InterfaceManager.Instance ? InterfaceManager.Instance.inGame : true;
        if (!inGame) {// Is in character selection screen.
            //_playerInput.transform.SetParent(characterChoice.charactersGroup);
            if (playersGameObjects.Contains(_playerInput.gameObject)) return; // Ensures this will not be executed twice.
            if (String.IsNullOrEmpty(_playerInput.currentControlScheme)) return;
            freeId[_playerInput.playerIndex] = false;
            if (_playerInput.TryGetComponent(out CharacterBoxUI characterBoxUI)) {
                characterBoxUI.playerConfig.id = _playerInput.playerIndex;
                characterBoxUI.playerConfig.controlScheme = _playerInput.currentControlScheme;
                characterBoxUI.playerConfig.inputDevices = GetStringFromDevices(_playerInput.devices.ToArray());
                characterBoxUI.characterImageBackground.color = PlayerColorLayerManager.GetColorBaseLight(_playerInput.playerIndex);
                
                characterBoxUI.UpdateTextTest();
                
                Animator animator = characterBoxUI.GetCurrentCharacterModels.GetComponentInChildren<Animator>();
                if (animator) animator.SetTrigger("connected");
            }
        } else {
            if (!cameraMovement) cameraMovement = FindAnyObjectByType<PrototypeCameraMoviment>();
            if (cameraMovement) PlayersToSendToCamera(_playerInput.transform);
            else
            {
                // Instantiate camera if dont has one
                ResourcesPrefabs resourcesPrefabs = Resources.Load<ResourcesPrefabs>("ResourcesPrefabs");
                PrototypeCameraMoviment prototypeCameraMoviment = resourcesPrefabs
                    .prefabs[(int)ResourcesPrefabs.PrefabType.Camera].GetComponent<PrototypeCameraMoviment>();
                cameraMovement = Instantiate(prototypeCameraMoviment);
                PlayersToSendToCamera(_playerInput.transform);
            }
        }
        if(!playersGameObjects.Contains(_playerInput.gameObject))
            playersGameObjects.Add(_playerInput.gameObject);
    }
    public void OnPlayerLeftEvent(PlayerInput _playerInput) { // Trigged when player left : set in inspector : PlayerInputManager
        if (_playerInput.currentControlScheme == "Keyboard&Mouse") keyboardP1 = false;
        if (_playerInput.currentControlScheme == "KeyboardP2") keyboardP2 = false;
        freeId[_playerInput.playerIndex] = true;
    }

    public void PlayersToSendToCamera(Transform p, bool isAlive = true)
    {
        if(isAlive)cameraMovement.RecivePlayers(p);
        else cameraMovement.RemovePlayers(p);
    }

    private int playersCountInScene = 0;
    public int ScoreToWinGame = 10;

    private const int scoreToAddToKill = 1;
    private const int scoreToAddToWinner = 0;

    bool gameOver;
    public bool GameOver
    {
        get => gameOver;
        set { gameOver = value; }
    }
    public int winnerSupreme = -1;
    public void AddPointsToPlayer(int playerWhoKilled,Transform p1, Transform p2)
    {
        playersCountInScene++;
        AddPoints(playerWhoKilled, scoreToAddToKill);

        if (playersCountInScene >= playersConfigs.Count-1 || CheckIfGameOver())
        {
            GameOver = true;
            InterfaceManager.Instance.isOnFeedback = true;
            InterfaceManager.Instance.GameOver();
            playersCountInScene = 0;
            //End Game
            // PowerUpManager.CreateBackup(playersGameObjects);
            CameraManager.Instance.OnEndFeedback = false;
            AddPoints(playerWhoKilled, scoreToAddToWinner);
            InterfaceManager.Instance.OnCallFeedbackGame(CheckIfGameOver());
            winnerSupreme = CheckIfGameOver()? playerWhoKilled:-1;
            //PlayersToSendToCamera(other.transform, true);

            foreach (var p in playersGameObjects)
            {
                if (p != null)
                {
                    var pC =p.GetComponent<Character.Character>();
                    pC.CharacterEntity.CharacterState.SetLoserState();
                    pC.CharacterEntity.CharacterMesh.animator.SetFloat("Speed", 0);
                }
            }
        }

        SavePlayersConfigs();
    }

    public void RemovePointsToPlayer(int playerWhoKilled)
    {
        playersCountInScene++;
        RemovePoints(playerWhoKilled, -scoreToAddToKill);

        if (playersCountInScene >= playersConfigs.Count-1)
        {
            GameOver = true;
            playersCountInScene = 0;
            //End Game
            // PowerUpManager.CreateBackup(playersGameObjects);
            CameraManager.Instance.OnEndFeedback = false;
            if(GameOver)
                CameraManager.Instance.DeathFeedBack();
            InterfaceManager.Instance.OnCallFeedbackGame(CheckIfGameOver());
            winnerSupreme = CheckIfGameOver()? playerWhoKilled:-1;
        }

        SavePlayersConfigs();
    }

    void AddPoints(int playerId, int value)
    {
        PlayerConfigurationData pS = playersConfigs[GetPlayerById(playerId)];
        pS.ChangeScore(value);
        playersConfigs[GetPlayerById(playerId)] = pS;

    }
    void RemovePoints(int playerId, int value)
    {
        PlayerConfigurationData pS = playersConfigs[GetPlayerById(playerId)];
        if (pS.score <= 0) return;
        pS.ChangeScore(value);
        playersConfigs[GetPlayerById(playerId)] = pS;
    }
    int GetPlayerById(int id){
        for (int i = 0; i < playersConfigs.Count; i++)
            if(playersConfigs[i].id == id) return i;

        return -1;
    }

    private List<PlayerConfigurationData> playersConfigsAux = new List<PlayerConfigurationData>();
    public bool CheckPlayerWinner(int id)
    {
        if (playersConfigs[GetPlayerById(id)].score == 0) return false;
        return id == winnerId;
    }

    public bool CheckIfGameOver()
    {
        foreach (var p in playersConfigs)
        {
            if (p.score >= ScoreToWinGame)
                return true;
        }

        return false;
    }

    public List<PlayerConfigurationData> ReturnPlayersList()
    {
        return playersConfigs;
    }
    #endregion

    #region Setters


    void SetPlayersConfigs(){
        foreach (var item in playersConfigs) {

            // Set player material
            PlayerInput playerInput = playerInputManager.JoinPlayer(item.id, controlScheme: item.controlScheme, pairWithDevices: GetDevicesFromString(item.inputDevices));
            OnPlayerJoinedEvent(playerInput);
            if (playerInput.TryGetComponent(out Character.Character character)){
                character.Id = item.id;
                character.transform.LookAt(-character.transform.forward, character.transform.up); // Made character look at camera direction
                //character.CharacterEntity.GrapplingHookTransform.gameObject.SetActive(false);
            }
            if (playerInput.TryGetComponent(out CharacterMesh characterMesh)) {
                characterMesh.SetMesh(item.characterModel);
                characterMesh.SetColor(item.characterColor);
                characterMesh.GetMeshParent.SetActive(false);
                characterMesh.CharacterEntity.GrapplingHookTransform.gameObject.SetActive(false);
                characterMesh.CharacterEntity.GrapplingHookRope.SetActive(false);
            }
            if (playersSpawners) {
                playerInput.transform.position = playersSpawners.GetSpawners[item.id].position;
            }

            /*if (!InterfaceManager.Instance.startNewGame)
                OnPlayerConfigAdd?.Invoke(item);*/
        }
        playerInputManager.DisableJoining();
        
        InterfaceManager.Instance.StartCount();
    }
    public void SetPlayerStatus(bool isReady) {
        if (isReady) {
            amountOfPlayersReady++;
        } else {
            amountOfPlayersReady--;
        }
        if (amountOfPlayersReady == playerInputManager.playerCount) {
            SavePlayersConfigs();
            canInitGame = playerInputManager.playerCount > 1;
        } else {
            canInitGame = false;
        }
    }
    public void RemovePlayerGameObject(GameObject playerGameObject){
        playersGameObjects.Remove(playerGameObject);
    }
    #endregion

    #region PlayersConfigs

    public void AddNewPlayerConfig(PlayerConfigurationData playerConfiguration) {
        playersConfigs.Add(playerConfiguration);
        OnUpdateText?.Invoke();
    }
    
    public void AddNewPlayerConfigAUX(PlayerConfigurationData playerConfiguration) {
        playersConfigsAUX.Add(playerConfiguration);
    }
    public void ChangePlayerConfigAUX(PlayerConfigurationData playerConfiguration) {

        for (int i = 0; i < playersConfigsAUX.Count; i++)
        {
            if (playersConfigsAUX[i].id == playerConfiguration.id)
                playersConfigsAUX[i] = playerConfiguration;
        }
       
    }
    public void RemovePlayerConfigAUX(PlayerConfigurationData playerConfiguration)
    {
        for (int i = 0; i < playersConfigsAUX.Count; i++)
        {
            if (playersConfigsAUX[i].id == playerConfiguration.id)
                playersConfigsAUX.RemoveAt(i);
        }
    }
    public void RemovePlayerConfig(PlayerConfigurationData playerConfiguration) {
        playersConfigs.Remove(playerConfiguration);
        OnUpdateText?.Invoke();
        //OnPlayerConfigRemove?.Invoke(playerConfiguration);
    }
    public void ClearPlayersConfig(bool charactersFromGame = false) {
        amountOfPlayersReady = 0;
        if (cameraMovement) cameraMovement.RemoveAllPlayers();
        playersConfigs.Clear();
        playersConfigsAUX.Clear();
        if (charactersFromGame) {
            for (int i = 0; i < playersGameObjects.Count; i++){
                if (playersGameObjects[i] && (playersGameObjects[i].layer != LayerMask.NameToLayer("UI"))){
                    Destroy(playersGameObjects[i]);
                }
            }
            playersGameObjects.Clear();
        } else {
            characterChoice.RemoveAllChildrens();
            playersGameObjects.Clear();
        }
    }

    public bool PlayerTypeIsAvailable(ECharacterType _characterType) {
        bool isAvailable = true;
        if (playersConfigs.Count == 0) return isAvailable;
        for (int i = 0; i < playersConfigs.Count; i++) {
            if (playersConfigs[i].characterModel == _characterType) {
                isAvailable = false;
                break;
            }
        }
        return isAvailable;
    }
    
    public bool PlayerTypeIsAvailable(int c) {
        bool isAvailable = true;
        if (playersConfigsAUX.Count == 0) return isAvailable;
        for (int i = 0; i < playersConfigsAUX.Count; i++) {
            if (playersConfigsAUX[i].characterIndexCharacterChoice == c) {
                {
                    isAvailable = false;
                }
                break;
            }
        }
        return isAvailable;
    }
    #endregion

    #region ActiveInputActions
    public void EnableInputActions() {
        actions.Navigation.Enable();
    }
    public void DisableInputActions() {
        actions.Navigation.Disable();
    }
    #endregion

    #region SaveAndLoad
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
    #endregion

    #region Getters
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

    public List<GameObject> PlayersGameObjects => playersGameObjects;
    #endregion
}

[Serializable]
public class PlayersInputData {
    public PlayersManager.PlayerConfigurationData[] playersConfigs;
}
