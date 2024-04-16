using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersScoreManager : MonoBehaviour
{
   [SerializeField]private GameObject playerScoreGameObject;
   private void OnEnable()
   {
      InterfaceManager.Instance.OnAddPlayersToList += AddPlayersToList;
   }

   private void OnDisable()
   {
      if(!InterfaceManager.Instance) return;
      InterfaceManager.Instance.OnAddPlayersToList -= AddPlayersToList;
   }
   public void AddPlayersToList()
   {
      foreach (var p in InterfaceManager.Instance.playerScores)
      {
         GameObject o = Instantiate(playerScoreGameObject, this.transform);
         PlayerScore pS = o.GetComponent<PlayerScore>();
         pS.data.id = p.id;
         pS.data.score = p.score;
         //InterfaceManager.Instance.playerScores.Add(pS);
      }
     
     
   }
}
