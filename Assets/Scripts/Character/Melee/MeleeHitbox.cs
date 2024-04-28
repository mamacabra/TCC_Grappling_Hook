using Character;
using Character.States;
using Character.Utils;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Character.Melee
{
    public class MeleeHitbox : ACharacterMonoBehaviour
    {
        [SerializeField] private Character character;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Character"))
            {
                var enemy = other.GetComponent<Character>();
                if (enemy == null)
                    return;
                if (enemy.CharacterEntity.CharacterState.State is DeathState)
                    return; 
                enemy.CharacterEntity.CharacterState.SetDeathState();
                PlayersManager.Instance.AddPointsToPlayer(character.Id);
                
            }
        }
    }

}