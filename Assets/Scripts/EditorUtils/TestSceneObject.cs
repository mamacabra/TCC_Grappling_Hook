using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSceneObject : MonoBehaviour
{
    void Start()
    {
        if(!PlayersManager.Instance){ // Para testar em qualquer cena remova o **&& false**
            ResourcesPrefabs playersManagerPrefabs = Resources.Load<ResourcesPrefabs>("ResourcesPrefabs");
            GameObject playersManagerPrefab = playersManagerPrefabs.prefabs[(int)ResourcesPrefabs.PrefabType.PlayersManager];
            PlayersManager playersManager = Instantiate(playersManagerPrefab.GetComponent<PlayersManager>(), gameObject.transform);
            playersManager.InitGame(loadScene: false);
        }
    }
}
