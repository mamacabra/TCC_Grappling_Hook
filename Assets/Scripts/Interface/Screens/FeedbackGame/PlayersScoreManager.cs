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
      AddPlayersToList();
   }

   public void AddPlayersToList()
   {
      if (childs.Count > 0)
      {
         foreach (var c in childs)
            Destroy(c);
         childs.Clear();
      }

      List<PlayersManager.PlayerConfigurationData> list = new List<PlayersManager.PlayerConfigurationData>();
      list = PlayersManager.Instance.ReturnPlayersList();

      StartCoroutine(PutPlayers());
      IEnumerator PutPlayers()
      {
         foreach (var p in list)
         {
            yield return new WaitForSeconds(0.25f);
            GameObject o = Instantiate(playerScoreGameObject, this.transform);
            childs.Add(o);
            PlayerScore pS = o.GetComponent<PlayerScore>();
            pS.ChangeData(p);
            yield return null;
         }
      }

      
   }
}
