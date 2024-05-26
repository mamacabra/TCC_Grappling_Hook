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
    [SerializeField] private Slider playeSlideScore;

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
        playeSlideScore.value = scoreValue;
        
        scoreValue = data.score;
    }

    public void AtualizeScore()
    {
        if (playeSlideScore)
        {
            playeSlideScore.DOValue(scoreValue, 1f).SetEase(Ease.OutBack).SetDelay(0.35f).OnComplete(() =>
            {
                //transform.DOShakeScale(0.5f, 0.25f);
            });
        }
    }
}
