using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CharacterChoiceScreen : Screens
{
    [SerializeField] private ButtonToScreen backToMenu, playGame;
    [SerializeField] private List<Transform> charactersGroup = new List<Transform>();
    [SerializeField] private PlayerInput playerInput1;
    [SerializeField] private PlayerInput playerInput2;
    [SerializeField] private List<PlayerInput> playerInputsGamePad = new List<PlayerInput>();
    [SerializeField] private Transform tutorial;
    [SerializeField] private Slider startGameSlider;
    public float sliderSpeed;

    private int objEnables = 0;

    [SerializeField] private TextMeshProUGUI playText;

    private bool startGame = false;
    private void Awake()
    {
        backToMenu.button.onClick.AddListener(delegate { GoToScreen(backToMenu.goToScreen); });
        playGame.button.onClick.AddListener(delegate { if (PlayersManager.Instance.CanInitGame) GoToScreen(playGame.goToScreen); });
    }

    public override void Initialize()
    {
        startGame =false;
        if (PlayersManager.Instance)
        {
            PlayersManager.Instance.characterChoice = this;
            PlayersManager.Instance.InitCharacterSelection();
        }

        objEnables = 0;
        EventSystem.current.SetSelectedGameObject(tutorial.gameObject);
        tutorial.SetParent(charactersGroup[0]);
        tutorial.gameObject.SetActive(true);
        
        if(current != null) StopCoroutine(current);
        current = StartCoroutine(ChangeText());
    }
    public override void Close()
    {
        if (PlayersManager.Instance)
        {
            PlayersManager.Instance.DisableInputActions();
        }
    }

    public override void GoToScreen(ScreensName screensName)
    {
        base.GoToScreen(screensName);
        startGame = true;
        if(current != null) StopCoroutine(current);
    }

    public PlayerInput ReturnPlayerInput(bool isGamePad = false, bool isP1 = false)
    {
        if (isP1)
        {
            CheckGroup(playerInput1.transform);
            playerInput1.gameObject.SetActive(true);
            return playerInput1;
        }
        if (!isGamePad)
        {
            CheckGroup(playerInput2.transform);
            playerInput2.gameObject.SetActive(true);
            return playerInput2;
        }

        foreach (var obj in playerInputsGamePad)
        {
            if (!obj.gameObject.activeSelf)
            {
                PlayerInput pGamePad = obj;
                CheckGroup(pGamePad.transform);
                pGamePad.gameObject.SetActive(true);
                return pGamePad;
            }
        }

        return null;
    }

    public void RemoveAllChildrens() {
        foreach (var item in playerInputsGamePad)
        {
            item.gameObject.SetActive(false);
            item.transform.SetParent(transform);
        }
        playerInput1.gameObject.SetActive(false);
        playerInput1.transform.SetParent(transform);
        playerInput2.gameObject.SetActive(false);
        playerInput2.transform.SetParent(transform);
        objEnables = 0;
    }
    
    public void CheckGroup(Transform obj, bool add = true)
    {
        if (add)
        {
            objEnables++;
            if(objEnables >6) objEnables = 6;
            if (objEnables == 3)
            {
                tutorial.SetParent(charactersGroup[1]);
                tutorial.SetAsLastSibling();
            }
            if(objEnables==6)
                tutorial.gameObject.SetActive(false);
            
            if (objEnables>3)
            {
                if (charactersGroup[0].childCount > 3)
                    charactersGroup[0].GetChild(charactersGroup[0].childCount-1).SetParent(charactersGroup[1]);
                obj.SetParent(charactersGroup[1]);
                obj.SetAsLastSibling();
                tutorial.SetAsLastSibling();
            }
            else
            {
                obj.SetParent(charactersGroup[0]);
                obj.SetAsLastSibling();
                tutorial.SetAsLastSibling();
            }
        }
        else
        {
            objEnablesAuxReorganize = 0;
            objEnables--;
            if(objEnables <0) objEnables = 0;
            obj.SetParent(transform.parent);
            List<Transform> objsInCharactersGroup1 = new List<Transform>();

            for (int i = 0; i < charactersGroup[0].childCount; i++)
            {
                if(charactersGroup[0].GetChild(i) != tutorial)
                    objsInCharactersGroup1.Add(charactersGroup[0].GetChild(i));
            }

            for (int j = 0; j < charactersGroup[1].childCount; j++)
            {
                if(charactersGroup[1].GetChild(j) != tutorial)
                    objsInCharactersGroup1.Add(charactersGroup[1].GetChild(j));
            }

            if (objsInCharactersGroup1.Count == 0)
            {
                tutorial.SetParent(charactersGroup[0]);
                tutorial.SetAsLastSibling();
                tutorial.gameObject.SetActive(true);
                return;
            }
            tutorial.gameObject.SetActive(false);
            foreach (var o in objsInCharactersGroup1)
            {
                ReorganizeGroup(o);
                //o.SetParent(charactersGroup[0]);
            }
            
        }
        
        if(objEnables <6)
            tutorial.gameObject.SetActive(true);
        
    }

    private int objEnablesAuxReorganize = 0;
    void ReorganizeGroup(Transform obj)
    {
        objEnablesAuxReorganize++;
        obj.SetParent(charactersGroup[0]);
        obj.SetAsLastSibling();
        tutorial.SetAsLastSibling();
        if (objEnablesAuxReorganize == 3)
        {
            tutorial.SetParent(charactersGroup[1]);
            tutorial.SetAsLastSibling();
            tutorial.gameObject.SetActive(true);
        }
        if(objEnablesAuxReorganize==6)
            tutorial.gameObject.SetActive(false);
        if (objEnablesAuxReorganize > 3)
        {
            if (charactersGroup[0].childCount > 2)
                charactersGroup[0].GetChild(charactersGroup[0].childCount-1).SetParent(charactersGroup[1]);
            obj.SetParent(charactersGroup[1]);
            obj.SetAsLastSibling();
            
            tutorial.SetAsLastSibling();
        }
        else if(objEnablesAuxReorganize < 3)
        {
            tutorial.SetParent(charactersGroup[0]);
            tutorial.SetAsLastSibling();
            tutorial.gameObject.SetActive(true);
        }
    }
    private void Update() {
        SetButtonStartSlider(-0.5f);
        
    }
    public void SetButtonStartSlider(float value){
        sliderSpeed = value;

        if (/*Mathf.Floor(startGameSlider.value / 0.03f) > Mathf.Floor((startGameSlider.value - Time.deltaTime) / 0.03f) &&*/ startGameSlider.value != 0)
        {
            AudioManager.audioManager.SliderTest(startGameSlider.value);
        }

        startGameSlider.value += Time.deltaTime * sliderSpeed;
        Mathf.Clamp(startGameSlider.value, startGameSlider.minValue, startGameSlider.maxValue);
        if (startGameSlider.value >= startGameSlider.maxValue) {
            if (PlayersManager.Instance.CanInitGame) { 
                GoToScreen(playGame.goToScreen);
                AudioManager.audioManager.SliderTest(0);
            } 
        }
    }

    private Coroutine current;
    IEnumerator ChangeText()
    {
        if(startGame) yield break;
        playText.text = "Jogar";
        yield return new WaitForSeconds(1.5f);
        playText.text = "Pressiona A/X";
        yield return new WaitForSeconds(1.5f);
        if(current != null) StopCoroutine(current);
        current = StartCoroutine(ChangeText());
    }
}