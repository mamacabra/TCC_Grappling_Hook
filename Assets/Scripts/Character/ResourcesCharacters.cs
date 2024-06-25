using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using System;

public enum ECharacterType{
    Sushi = 0,
    Salmon = 1,
    Roulade = 2,
    Churros = 3,
    Pancake = 4,
    Sausage_Roll = 5,
    Count = 6,
}
[Serializable]
public struct SCharacterData{
    public string characterName;
    public ECharacterType characterType;
    public GameObject characterPrefab;
    public GameObject deathCharacterPrefab;
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