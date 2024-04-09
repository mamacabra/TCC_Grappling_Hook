using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterChoiseScreen : Screens
{
    [SerializeField] private ButtonToScreen backToMenu, playGame;
    public Transform charactersGroup;
    
    private void Awake()
    {
        backToMenu.button.onClick.AddListener(delegate { GoToScreen(backToMenu.goToScreen); });
        playGame.button.onClick.AddListener(delegate { GoToScreen(playGame.goToScreen); });
    }

    public override void Initialize()
    {
        if (PlayersManager.Instance)
        {
            PlayersManager.Instance.characterChoice = this;
            PlayersManager.Instance.InitCharacterSelection();
        }
    }
    public override void Close()
    {
        if (PlayersManager.Instance)
        {
            PlayersManager.Instance.Disable();
        }
    }

    public override void GoToScreen(ScreensName screensName)
    {
        if (PlayersManager.Instance.CanInitGame)
        {
            base.GoToScreen(screensName);
        }
    }

    private int playerIndex = 0;
    private List<CharacterBoxUI> playerConfigurationDataList = new List<CharacterBoxUI>();
    private Vector3 pos = new Vector3(0, 0, 0);
    private int posX = 0, posY = 0;
    public void OrganizeGrid(bool playerJoin, CharacterBoxUI characterBoxUI)
    {
        playerIndex += playerJoin ? +1 : -1;
        if (!playerJoin)
            playerConfigurationDataList.Remove(characterBoxUI);
        else
            playerConfigurationDataList.Add(characterBoxUI);
        
        int i = 0;
        switch (playerIndex)
        {
            case 1:
                pos = Vector3.one;
                foreach (var p in playerConfigurationDataList)
                    p.transform.localPosition = pos;
                break;
            case 2:
                posX = -150;
                posY = 0;
                foreach (var p in playerConfigurationDataList)
                {
                    pos = new Vector3(posX, posY, 0);
                    p.transform.localPosition = pos;
                    posX = 150;
                }
                break;
            case 3:
                posX = -300;
                posY = 0;
                foreach (var p in playerConfigurationDataList)
                {
                    pos = new Vector3(posX, posY, 0);
                    p.transform.localPosition = pos;
                    posX += 150;
                }
                break;
            case 4:
                posX = -450;
                posY = 0;
                foreach (var p in playerConfigurationDataList)
                {
                    pos = new Vector3(posX, posY, 0);
                    p.transform.localPosition = pos;
                    posX += 150;
                }
                break;
            case 5:
                posX = -300;
                posY = 0;
                i = 0;
                foreach (var p in playerConfigurationDataList)
                {
                    i++;
                    pos = new Vector3(posX, posY, 0);
                    p.transform.localPosition = pos;
                    posX += 150;
                    if (i == 4)
                    {
                        posY += 150;
                        posX = -150;
                    }
                }
                break;
            case 6:
                posX = -300;
                posY = 0;
                i = 0;
                foreach (var p in playerConfigurationDataList)
                {
                    i++;
                    pos = new Vector3(posX, posY, 0);
                    p.transform.localPosition = pos;
                    posX += 150;
                    if (i == 4)
                    {
                        posY += 150;
                        posX = -300;
                    }
                }
                break;
        }
    }
}
