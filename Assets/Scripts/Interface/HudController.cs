using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class HudController : MonoBehaviour
{
   [Header("Count")] 
   [SerializeField] private GameObject countGameObj;
   [SerializeField] private float timeToWaitToStartCount = 3;
   [SerializeField] private TextMeshProUGUI countGameStartText;
   [SerializeField] private string textToShowWhenCountOver = "VAI!";

   [SerializeField] private List<Color32> colorsToChangeCountText;
   
   private void OnEnable()
   {
      StartCoroutine(WaitToStartCount());
      IEnumerator WaitToStartCount()
      {
         yield return new WaitForSeconds(timeToWaitToStartCount);

         countGameStartText.transform.localScale = Vector3.one;
         
         countGameObj.SetActive(true);
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
        
         countGameStartText.text = textToShowWhenCountOver;
         countGameStartText.color = colorsToChangeCountText[0];

         countGameStartText.transform.DOScale(1.1f, 0.25f).SetEase(Ease.OutBack).OnComplete(() =>
         {
            countGameStartText.transform.DOScale(1f, 0.25f).SetEase(Ease.OutBack);
         });
         
         yield return new WaitForSeconds(0.75f);
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
      }
   }
}
