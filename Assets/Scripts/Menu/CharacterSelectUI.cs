using UnityEngine;

public class CharacterSelectUI : MonoBehaviour 
{
    public Transform charactersGroup;
    private void Start() {
        if(PlayersManager.Instance) PlayersManager.Instance.characterSelect = this;
    }
}