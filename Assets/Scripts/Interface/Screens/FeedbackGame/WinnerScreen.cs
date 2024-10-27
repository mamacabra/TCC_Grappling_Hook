using System;
using System.Collections;
using System.Collections.Generic;
using Character.Utils;
using SceneSelect;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WinnerScreen : Screens
{
   // [SerializeField]private PlayerScore playerScore;
    [SerializeField] private ButtonToScreen initialScreenButton;
    //[SerializeField] private Image winner;
    private bool canclick;
    [SerializeField] private RawImage bg;
    [SerializeField] private List<Texture> bgsList = new List<Texture>();
    private void Awake()
    
    {
        initialScreenButton.button.onClick.AddListener(delegate 
        {
            GoToScreen(initialScreenButton.goToScreen);
            InterfaceManager.Instance.startNewGame = false;
            InterfaceManager.Instance.inGame = false;
            //InterfaceManager.Instance.playerScores.Clear();
            ScenesManager.Instance.UnloadCurrentScene();         
            
        });
    }
    
   
    public override void Initialize()
    {
        initialScreenButton.button.gameObject.SetActive(false);
        base.Initialize();
        List<PlayersManager.PlayerConfigurationData> list = new List<PlayersManager.PlayerConfigurationData>();
        list = PlayersManager.Instance.ReturnPlayersList();
        
        foreach (var w in list)
        {
            if (w.score >= PlayersManager.Instance.ScoreToWinGame)
            {
                ChangeModelImage(w);
                //winner.sprite = Resources.Load<ResourcesCharacters>("ResourcesCharacters").GetCharacterData(w.characterModel).characterSprite;
            }
        }

       
        StartCoroutine(WaitToCanClick());
        IEnumerator WaitToCanClick()
        {
            yield return new WaitForSeconds(2f);
            canclick = true;
            initialScreenButton.button.gameObject.SetActive(true);
        }
        //playerScore.ChangeData(p);
        EventSystem.current.SetSelectedGameObject(initialScreenButton.button.gameObject);
    }
    
    [SerializeField] private Image playerImg;
    [SerializeField] private GameObject[] characterModels;
    [SerializeField] private RawImage characterRawImage;

    [SerializeField] private List<RenderTexture> text = new List<RenderTexture>();
    [SerializeField] public  Camera cam;
    public void ChangeModelImage(PlayersManager.PlayerConfigurationData id) {

        foreach (var o in characterModels)
            o.SetActive(false);
       
     
        characterModels[(int)id.characterModel].SetActive(true);
        characterRawImage.texture = text[(int)id.characterModel];
        cam.targetTexture = text[(int)id.characterModel];
        bg.texture = bgsList[id.id];
        Animator animator = characterModels[(int)id.characterModel].GetComponentInChildren<Animator>();
        if (animator) animator.SetBool("isWinner", true);
        
        playerImg.color = PlayerColorLayerManager.GetColorBase(id.id);
    }
}