using UnityEngine;
using LocalMultiplayer;

public class PlayersSpawners : MonoBehaviour
{
    [SerializeField] private Transform[] spawners;
    [SerializeField] private Transform[] sortedSpawners;

    private void Awake(){
        SortSpawners();
        PlayersManager.Instance.playersSpawners = this;
    }

    public void SortSpawners() {
        sortedSpawners = spawners;
        int n = sortedSpawners.Length;
        for (int i = 0; i < n; i++) {
            // Swap array[i] with a random element in the array
            int randomIndex = i + Random.Range(0, n - i);
            Transform temp = sortedSpawners[randomIndex];
            sortedSpawners[randomIndex] = sortedSpawners[i];
            sortedSpawners[i] = temp;
        }
    }

    public Transform[] GetSpawners => sortedSpawners;
}
