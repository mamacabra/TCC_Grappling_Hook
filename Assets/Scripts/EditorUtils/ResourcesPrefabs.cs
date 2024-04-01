using System.Collections.Generic;
using UnityEngine;

public enum CharaterModel{
    Sushi = 0,
    Capsule = 1,
    Box = 2,
    Sphere = 3
}
[CreateAssetMenu(fileName = "ResourcesPrefabs", menuName = "ResourcesPrefabs", order = 0)]
public class ResourcesPrefabs : ScriptableObject {
    public enum PrefabType{
        PlayersManager = 0,
        Player = 1,
        PlayerUI = 2,
        Camera = 3
    }

    public List<GameObject> prefabs;
}