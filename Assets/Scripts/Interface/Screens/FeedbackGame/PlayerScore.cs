using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    public PlayersManager.PlayerConfigurationData data;
    [SerializeField] private Image playerImg;
    [SerializeField] private Slider playeSlideScore;

    private void Start()
    {
        playerImg.sprite = data.sCharacterData.characterSprite;
        playeSlideScore.value = data.score;
    }
}
