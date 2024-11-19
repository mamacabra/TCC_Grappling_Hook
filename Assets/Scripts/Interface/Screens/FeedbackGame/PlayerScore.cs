using System;
using System.Collections;
using System.Collections.Generic;
using Character.Utils;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using LocalMultiplayer;
using LocalMultiplayer.Data;

public class PlayerScore : MonoBehaviour
{
    public  PlayerConfigurationData data;
    [SerializeField] private Image playerImg;
    //[SerializeField] private Slider playeSlideScore;
    [SerializeField]private List<Image> pointsImages;
    [SerializeField] private Sprite caveira, caveiraOp;

    [SerializeField] public GameObject coroa;
    private int scoreValue = 0;

    [SerializeField] private Image bgImg;
    [SerializeField] private Image bgDarkImg;
    private void OnEnable()
    {
        InterfaceManager.Instance.OnShowScoreInFeedbackScreen += AtualizeScore;
      
    }

    private void OnDisable()
    {
        if(!InterfaceManager.Instance)return;
        InterfaceManager.Instance.OnShowScoreInFeedbackScreen -= AtualizeScore;
    }
    [SerializeField] private GameObject[] characterModels;
    [SerializeField] private RawImage characterRawImage;

    [SerializeField] private List<RenderTexture> text = new List<RenderTexture>();
    [SerializeField] public Camera cam;
    public void ChangeModelImage(int id) {
        Transform childTransform = characterModels[(int)data.characterModel].transform.GetChild(0);
        Animator animator = childTransform.GetComponent<Animator>();
        if (animator) 
        {
            if (animator.GetBool("isWinner"))
            { 
                animator.SetBool("isWinner", false); 
            }
        } 
        
        foreach (var o in characterModels)
            o.SetActive(false);
       
        characterModels[(int)data.characterModel].SetActive(true);
        characterRawImage.texture = text[(int)data.characterModel];
        cam.targetTexture = text[(int)data.characterModel];
      
        if (animator) animator.SetTrigger("connected");
        
        playerImg.color = PlayerColorLayerManager.GetColorBase(id);
        bgImg.color =  PlayerColorLayerManager.GetColorBase(id);
        bgDarkImg.color = PlayerColorLayerManager.GetColorBaseDark(id);
    }
    public void ChangeData(PlayerConfigurationData d)
    {
        data = d;
       //playerImg.sprite = Resources.Load<ResourcesCharacters>("ResourcesCharacters").GetCharacterData(data.characterModel).characterSprite;
       // playerImg.color = PlayersManager.GetColor(data.characterColor);
       foreach (var p in pointsImages)
       {
           p.sprite = caveiraOp;
           p.color =  PlayerColorLayerManager.GetColorBase(d.id);;
       }

        scoreValue = data.score;

        ChangeModelImage(data.id);
    }

    public void AtualizeScore()
    {
        for (int i = 0; i < scoreValue; i++)
        {
            if (scoreValue <= PlayersManager.Instance.ScoreToWinGame)
            {
                pointsImages[i].sprite = caveira;
            }
        }
        
        
    }
}
