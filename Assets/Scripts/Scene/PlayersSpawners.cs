using UnityEngine;

public class PlayersSpawners : MonoBehaviour
{
    public Transform[] spawners;

    private void Awake(){
        PlayersManager.Instance.playersSpawners = this;
    }
}
