using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersScoreManager : MonoBehaviour
{
   [SerializeField]private List<PlayerScore> playerScores = new List<PlayerScore>();
   [SerializeField]private GameObject playerScoreGameObject;
   private void OnEnable()
   {
      PlayersManager.Instance.OnPlayerConfigAdd += AddPlayersToList;
      PlayersManager.Instance.OnPlayerConfigRemove += RemovePlayers;
      PlayersManager.Instance.OnPlayerDeath += OnPlayerDeath;

      if (playerScores.Count > 0)
         playerScores.Clear();
   }

   private void OnDisable()
   {
      if (!PlayersManager.Instance) return;
      PlayersManager.Instance.OnPlayerConfigAdd -= AddPlayersToList;
      PlayersManager.Instance.OnPlayerConfigRemove -= RemovePlayers;
      PlayersManager.Instance.OnPlayerDeath -= OnPlayerDeath;
   }

   public void AddPlayersToList(PlayersManager.PlayerConfigurationData playerData)
   {
      GameObject p = Instantiate(playerScoreGameObject);
      PlayerScore pS = p.GetComponent<PlayerScore>();
      pS.data.id = playerData.id;
      pS.data.score = playerData.score;
      playerScores.Add(pS);
   }
   public void RemovePlayers(PlayersManager.PlayerConfigurationData playerData)
   {
      PlayerScore pS =playerScores.Find(p => p.data.id == playerData.id);
      playerScores.Remove(pS);
   }

   public void OnPlayerDeath(int id)
   {
      PlayerScore pS =playerScores.Find(p => p.data.id == id);
      pS.data.score += 10;
   }
}
