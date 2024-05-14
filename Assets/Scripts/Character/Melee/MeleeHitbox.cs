using Character.States;
using Character.Utils;
using UnityEngine;

namespace Character.Melee
{
    public class MeleeHitbox : ACharacterMonoBehaviour
    {
        [SerializeField] private Character character;

        private void OnTriggerEnter(Collider other)
        {
            if (character.CharacterEntity.CharacterState.State is DeathState) return;
            if (other.CompareTag("Character") == false) return;

            var enemy = other.GetComponent<Character>();
            if (enemy == null) return;
            if (enemy.CharacterEntity.CharacterState.State is DeathState) return;

            if (enemy.CharacterEntity.CharacterState.State is AttackState)
            {   
                enemy.CharacterEntity.CharacterState.SetKnockbackState();
                character.CharacterEntity.CharacterState.SetKnockbackState();
            }
            else
            {
                enemy.CharacterEntity.CharacterState.SetDeathState();
                PlayersManager.Instance.AddPointsToPlayer(character.Id);
            }
        }
    }
}