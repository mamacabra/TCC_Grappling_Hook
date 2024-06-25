using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersScoreManager : MonoBehaviour
{
   [SerializeField]private GameObject playerScoreGameObject;
   private List<GameObject> childs = new List<GameObject>();
   private List<PlayerScore> childsScore = new List<PlayerScore>();
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

      childsScore.Clear();
      List<PlayersManager.PlayerConfigurationData> list = new List<PlayersManager.PlayerConfigurationData>();
      list = PlayersManager.Instance.ReturnPlayersList();

      StartCoroutine(PutPlayers());
      IEnumerator PutPlayers()
      {
         yield return new WaitForSeconds(0.5f);
         int maxScore = 0;
         foreach (var p in list)
         {
            //yield return new WaitForSeconds(0.5f);
            GameObject o = Instantiate(playerScoreGameObject, this.transform);
            childs.Add(o);
            PlayerScore pS = o.GetComponent<PlayerScore>();
            pS.ChangeData(p);

           
            //yield return null;
         }
         childs.Sort((x, y) => y.GetComponent<PlayerScore>().data.score.CompareTo(x.GetComponent<PlayerScore>().data.score));

         foreach (var obj in childs)
         {
            obj.transform.SetAsLastSibling();
            obj.GetComponent<PlayerScore>().coroa.SetActive(false);
         }

         childs[0].GetComponent<PlayerScore>().coroa.SetActive(true);
         /*if (p.score > maxScore)
         {
            maxScore = p.score;
            o.transform.SetAsFirstSibling();
            
            foreach (var obj in childs)
               obj.GetComponent<PlayerScore>().coroa.SetActive(false);
            
            o.GetComponent<PlayerScore>().coroa.SetActive(true);
            
         }*/

         yield return new WaitForSeconds(1f);
         
         InterfaceManager.Instance.ShowScoreInFeedbackScreen();
      }

      
   }
}
