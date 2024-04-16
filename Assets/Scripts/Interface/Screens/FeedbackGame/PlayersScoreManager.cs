using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersScoreManager : MonoBehaviour
{
   [SerializeField]private GameObject playerScoreGameObject;
   private List<GameObject> childs = new List<GameObject>();
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
      if (childs.Count > 0)
      {
         foreach (var c in childs)
            Destroy(c);
         childs.Clear();
      }
      
      foreach (var p in InterfaceManager.Instance.playerScores)
      {
         GameObject o = Instantiate(playerScoreGameObject, this.transform);
         childs.Add(o);
         PlayerScore pS = o.GetComponent<PlayerScore>();
         pS.ChangeData(p);
      }
   }
}
