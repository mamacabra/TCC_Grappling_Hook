using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.Serialization;

public class CharacterBoxUI : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Image characterImage;
    [SerializeField] private GameObject[] characterModels;
    public Image characterImageBackground;
    [SerializeField] private TextMeshProUGUI characterName;
    [SerializeField] private TextMeshProUGUI characterStatus;
    private bool hasConfirmed;
    private bool pressed = false;

    public PlayersManager.PlayerConfigurationData playerConfig;

    public GameObject GetCurrentCharacterModels => characterModels[(int)playerConfig.characterModel];

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

        if (dir_x != 0)
        {
            ChangeModelImage(dir_x);
        }
        // else
        //     ChangeColor(dir_y);
    }

    public void OnConfirm(InputAction.CallbackContext context) {
        if(PlayersManager.Instance.debug) return;
        if (hasConfirmed) {
            if(context.performed){
                pressed = true;
            }
            if(context.canceled){
                pressed = false;
                // choiceScreen.SetButtonStartSlider(-(pressTime / 2));
                // pressTime = 0.0f;
            }
        }

        if(hasConfirmed) return;
        if (!PlayersManager.Instance.PlayerTypeIsAvailable(playerConfig.characterModel))
        {
            characterStatus.text = "Já escolhido";
            characterStatus.color = Color.red;

            return;
        }


        if (context.action.WasPerformedThisFrame()) {
            characterStatus.text = "Pronto";
            characterStatus.color = Color.green;
            hasConfirmed = true;
            PlayersManager.Instance?.AddNewPlayerConfig(playerConfig);
            PlayersManager.Instance?.SetPlayerStatus(true);

            Animator animator = GetCurrentCharacterModels.GetComponentInChildren<Animator>();
            if (animator) animator.SetTrigger("selected");
            
        }
    }

    [FormerlySerializedAs("choiseScreen")] [SerializeField] private CharacterChoiceScreen choiceScreen;
    public void OnCancel(InputAction.CallbackContext context) {
        if (context.action.WasPerformedThisFrame()) {
            if (!hasConfirmed) {
                if(!gameObject.activeSelf) return;
                PlayersManager.Instance?.RemovePlayerConfigAUX(playerConfig);
                PlayersManager.Instance?.RemovePlayerGameObject(gameObject);
                choiceScreen.CheckGroup(transform, false);
                gameObject.SetActive(false);
                return; 
            }
            characterStatus.text = "Escolhendo";
            characterStatus.color = Color.gray;
            PlayersManager.Instance?.RemovePlayerConfig(playerConfig);
            PlayersManager.Instance?.SetPlayerStatus(false);
            hasConfirmed = false;
        }
    }

    public void OnDeviceLost(PlayerInput playerInput){
        //if(!gameObject.activeSelf) return;
        characterStatus.text = "Pressione qualquer botão";
        characterStatus.color = Color.gray;
        PlayersManager.Instance?.RemovePlayerConfig(playerConfig);
        PlayersManager.Instance?.RemovePlayerConfigAUX(playerConfig);
        PlayersManager.Instance?.SetPlayerStatus(false);
        hasConfirmed = false;

        gameObject.SetActive(false);
        PlayersManager.Instance?.RemovePlayerGameObject(gameObject);
        choiceScreen.CheckGroup(transform, false);
    }

    public void UpdateText()
    {
        if (hasConfirmed) return;
        if (!PlayersManager.Instance.PlayerTypeIsAvailable(playerConfig.characterModel))
        {
            characterStatus.text = "Já escolhido";
            characterStatus.color = Color.red;
        }
        else
        {
            characterStatus.text = "Escolhendo";
            characterStatus.color = Color.gray;
        }
    }
    public void UpdateTextTest()
    {
        int v = 0;
        CheckCharacter(v);
        
        characterStatus.text = "Escolhendo";
        characterStatus.color = Color.gray;
    }

    void CheckCharacter(int v)
    {
        if (v < 0) v =  (int)ECharacterType.Count - 1;
        if (v > (int)ECharacterType.Count - 1) v = 0;

        int vAux = v;
       
        if (!PlayersManager.Instance.PlayerTypeIsAvailable(v))
        {
            vAux++;
            CheckCharacter(vAux);
        }
        else
        {

            foreach (var c in characterModels)
                c.SetActive(false);
            
            characterModels[vAux].SetActive(true);
            
            playerConfig.characterIndexCharacterChoice = vAux;
            playerConfig.characterModel = (ECharacterType)vAux;
            PlayersManager.Instance?.AddNewPlayerConfigAUX(playerConfig);
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
        characterModels[(int)playerConfig.characterModel].SetActive(false);
        int value = (int)playerConfig.characterModel + dir;
        if (value < 0) value =  (int)ECharacterType.Count - 1;
        if (value > (int)ECharacterType.Count - 1) value = 0;
        playerConfig.characterModel = (ECharacterType)value;
        characterModels[value].SetActive(true);
        playerConfig.characterIndexCharacterChoice = value;
        if(gameObject.activeSelf)
            PlayersManager.Instance.ChangePlayerConfigAUX(playerConfig);
        //Animator animator = characterModels[value].GetComponentInChildren<Animator>();
        //if (animator) animator.SetTrigger("connected");

        // @TODO: Luan Colocar som quando troca personagem
        //Sprite sprite = Resources.Load<ResourcesCharacters>("ResourcesCharacters").GetCharacterData((ECharacterType)value).characterSprite;
        //characterImage.sprite = sprite;

        if (!PlayersManager.Instance.PlayerTypeIsAvailable(playerConfig.characterModel))
        {
            characterStatus.text = "Já escolhido";
            characterStatus.color = Color.red;
            
            ChangeModelImage(dir);
        }
        else
        {
            characterStatus.text = "Escolhendo";
            characterStatus.color = Color.gray;
        }
    }

    private void OnEnable()
    {
        PlayersManager.Instance.OnUpdateText+= UpdateText;
        UpdateText();
        //UpdateTextTest();
    }

    private void OnDisable() {
        characterStatus.text = "Pressione qualquer botão";
        characterStatus.color = Color.gray;
        hasConfirmed = false;
        playerConfig = new PlayersManager.PlayerConfigurationData();
        // ChangeColor((int) playerConfig.characterColor);
        for (int i = 0; i < characterModels.Length; i++) {
            characterModels[i].SetActive(false);
        }
        //ChangeModelImage((int)ECharacterType.Sushi);

        if (PlayersManager.Instance)
            PlayersManager.Instance.OnUpdateText-= UpdateText;
    }

    private void Update() {
        if (pressed)
        {
            float currentValue = choiceScreen.sliderSpeed;
            float valueToAdd = 0.5f;
            if (currentValue == -0.5f) valueToAdd += 1.0f;
            choiceScreen.SetButtonStartSlider(choiceScreen.sliderSpeed + valueToAdd);
        }
    }
}
