using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using System;

public enum ECharacterType{
    Sushi = 0,
    Capsule = 1,
    Cube = 2,
    Sphere = 3,
    Count = 4
}
[Serializable]
public struct SCharacterData{
    public string characterName;
    public ECharacterType characterType;
    public GameObject characterPrefab;
    public Sprite characterSprite;
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