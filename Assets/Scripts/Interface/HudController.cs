using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using PowerUp;
using TMPro;
using UnityEngine;

public class HudController : MonoBehaviour
{
   [Header("Count")]
   [SerializeField] private GameObject countGameObj;
   [SerializeField] private float timeToWaitToStartCount = 3;
   private const float timeToSpawnCharacter = 0.75f;
   [SerializeField] private TextMeshProUGUI countGameStartText;
   [SerializeField] private string textToShowWhenCountOver = "VAI!";
   [SerializeField] private string textToShowWhenCountOverMatchPoint = "MATCH POINT!";
   [SerializeField] private List<Color32> colorsToChangeCountText;

   private List<GameObject> players = new List<GameObject>();

   [SerializeField] private GameObject matchPointFeedback;
   [HideInInspector]public bool gameAlreadyStarted = false;
  
   private void OnEnable()
   {
      
      matchPointFeedback.SetActive(false);
      if (InterfaceManager.Instance)
      {
         InterfaceManager.Instance.OnStartCount += StartCount;
         InterfaceManager.Instance.OnRestartGame += RestarGame;
      }
      
      if (!InterfaceManager.Instance.inGame)
      {
         InterfaceManager.Instance.isOnCount = true;
         InterfaceManager.Instance.inGame = true;
        
      }
      else
      {
         timeToWaitToStartCount = 0;
         for (int i = 0; i < players.Count; i++) {
            if (players[i].TryGetComponent(out Character.Character _character)) {
               _character.CharacterEntity.CharacterState.SetReadyState();
            }
         }
         StartCoroutine(WaitToStartCount(0));
      }
   }

   private void OnDisable()
   {
      if (InterfaceManager.Instance)
      {
         InterfaceManager.Instance.OnStartCount -= StartCount;
         InterfaceManager.Instance.OnRestartGame -= RestarGame;
      }
   }

   public void RestarGame()
   {
      gameAlreadyStarted = false;
   }
   
   public void StartCount()
   {
      players = PlayersManager.Instance.PlayersGameObjects;
      timeToWaitToStartCount = players.Count;
      StartCoroutine(InitPlayers());
      StartCoroutine(WaitToStartCount(2));
      
   }

   IEnumerator InitPlayers()
   {
      for (int i = 0; i < players.Count; i++) {
         if (players[i].TryGetComponent(out Character.Character _character)) {
            _character.CharacterEntity.CharacterState.SetReadyState();
         }
      }

      yield return new WaitForSeconds(1);

      for (int i = 0; i < players.Count; i++) {
         if (players[i].TryGetComponent(out Character.Character _character)) {
            yield return new WaitForSeconds(timeToSpawnCharacter);
            _character.PlaySpawnAnims();
         }
      }
   }

   IEnumerator WaitToStartCount(float plusTime)
   {
      yield return new WaitForSeconds((timeToSpawnCharacter * timeToWaitToStartCount)+1.5f);

      countGameStartText.transform.localScale = Vector3.one;

      countGameObj.SetActive(true);
      AudioManager.audioManager.PlayUiSoundEffect(UiSoundsList.GameCountDown);
      for (int i = 3; i > 0; i--)
      {
         countGameStartText.text = i.ToString();
         countGameStartText.color = colorsToChangeCountText[i];
         countGameStartText.transform.DOScale(1.1f, 0.25f).SetEase(Ease.OutBack).OnComplete(() =>
         {
            countGameStartText.transform.DOScale(1f, 0.25f).SetEase(Ease.OutBack);
         });

         yield return new WaitForSeconds(0.75f);
         yield return null;
            
      }

      if (!gameAlreadyStarted)
      {
            
         gameAlreadyStarted = true;
         List<PlayersManager.PlayerConfigurationData> list = new List<PlayersManager.PlayerConfigurationData>();
         list = PlayersManager.Instance.ReturnPlayersList();

         List<PlayersManager.PlayerConfigurationData> listAux = new List<PlayersManager.PlayerConfigurationData>();
         foreach (var l in list)
         {
            if (l.score >= PlayersManager.Instance.ScoreToWinGame - 1)
               listAux.Add(l);
         }

         string finalText = textToShowWhenCountOver;
         if (listAux.Count > 0)
         {
            finalText = textToShowWhenCountOverMatchPoint;
            matchPointFeedback.SetActive(true);
         }

         countGameStartText.text = finalText;

         countGameStartText.color = colorsToChangeCountText[0];

         countGameStartText.transform.DOScale(1.1f, 0.25f).SetEase(Ease.OutBack).OnComplete(() =>
         {
            countGameStartText.transform.DOScale(1f, 0.25f).SetEase(Ease.OutBack);
         });
        
         yield return new WaitForSeconds(0.75f);
      }

      countGameStartText.transform.DOScale(0f, 0.25f).OnComplete(() =>
      {
         countGameObj.SetActive(false);
      });


      //Start game
      List<GameObject> players = PlayersManager.Instance.PlayersGameObjects;
      for (int i = 0; i < players.Count; i++) {
         if (players[i].TryGetComponent(out Character.Character _character))
            _character.CharacterEntity.CharacterState.SetWalkState();
      }

      // PowerUpManager.Instance.StartSpawn();
      yield return new WaitForSeconds(0.5f);
      InterfaceManager.Instance.isOnCount = false;

      InterfaceManager.Instance.pause = false; ///////////////// @PAUSEISSUE CARALHO SE UM DIA A GENTE TIRAR O 3,2,1 DO PAUSE O NOSSO JOGO NÃO VAI DESPAUSAR
   }
}
