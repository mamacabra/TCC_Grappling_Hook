using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    PlayersManager.PlayerConfigurationData data;
    [SerializeField] private Image playerImg;
    //[SerializeField] private Slider playeSlideScore;
    [SerializeField]private List<Image> pointsImages;
    [SerializeField] private Sprite caveira, caveiraOp;
    private int scoreValue = 0;
    private void OnEnable()
    {
        InterfaceManager.Instance.OnShowScoreInFeedbackScreen += AtualizeScore;
    }

    private void OnDisable()
    {
        if(!InterfaceManager.Instance)return;
        InterfaceManager.Instance.OnShowScoreInFeedbackScreen -= AtualizeScore;
    }

    public void ChangeData(PlayersManager.PlayerConfigurationData d)
    {
        data = d;
        playerImg.sprite = Resources.Load<ResourcesCharacters>("ResourcesCharacters").GetCharacterData(data.characterModel).characterSprite;
        playerImg.color = PlayersManager.GetColor(data.characterColor);
        foreach (var p in pointsImages) p.sprite = caveiraOp;

        scoreValue = data.score;
    }

    public void AtualizeScore()
    {
        for (int i = 0; i < scoreValue; i++)
            pointsImages[i].sprite = caveira;
    }
}
