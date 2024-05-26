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

    private void Start()
    {
        ;
    }
    

    public void ChangeData(PlayersManager.PlayerConfigurationData d)
    {
        data = d;
        playerImg.sprite = Resources.Load<ResourcesCharacters>("ResourcesCharacters").GetCharacterData(data.characterModel).characterSprite;
        playerImg.color = PlayersManager.GetColor(data.characterColor);
        if (playeSlideScore)
        {
            playeSlideScore.DOValue(data.score, 0.5f).SetEase(Ease.OutBack).SetDelay(0.5f).OnComplete(() =>
            {
                //transform.DOShakeScale(0.5f, 0.25f);
            });
        }
    }
}
