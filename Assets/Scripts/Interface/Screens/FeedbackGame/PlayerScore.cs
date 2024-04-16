using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    PlayersManager.PlayerConfigurationData data;
    [SerializeField] private Image playerImg;
    [SerializeField] private Slider playeSlideScore;

    private void Start()
    {
       
    }

    public void ChangeData(PlayersManager.PlayerConfigurationData d)
    {
        data = d;
        playerImg.sprite = data.sCharacterData.characterSprite;
        playerImg.color = PlayersManager.GetColor(data.characterColor);
        if(playeSlideScore)
            playeSlideScore.value = data.score;
    }
}
