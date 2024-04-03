using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using System;

public enum ECharacterType{
    Sushi = 0,
    Capsule = 1,
    Box = 2,
    Sphere = 3,
    Count = 4
}
[Serializable]
public struct SCharacterData{
    public ECharacterType characterType;
    public GameObject characterPrefab;
    public Sprite characterSprite;
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

[CreateAssetMenu(fileName = "ResourcesCharacters", menuName = "ResourcesCharacters", order = 0)]
public class ResourcesCharacters : ScriptableObject {
    public List<SCharacterData> characters;
    public SCharacterData GetCharacterData(ECharacterType characterType){
        SCharacterData result = default;
        for (int i = 0; i < characters.Count; i++)
        {
            if (characters[i].characterType == characterType) {
                result = characters[i];
                break;
            }
        }
        Assert.IsFalse(result.Equals(default), $"Can't find {characterType.ToString()} in characters list");
        return result;
    }
}