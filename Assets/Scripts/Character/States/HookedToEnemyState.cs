using Character.Utils;
using UnityEngine;

namespace Character.States
{
    public class HookedToEnemyState : ACharacterState
    {
        private readonly Vector3 _enemyPosition;

        public HookedToEnemyState(CharacterEntity characterEntity, Vector3 enemyPosition) : base(characterEntity)
        {

            _enemyPosition = enemyPosition;
            CharacterEntity.Character.characterBody.LookAt(_enemyPosition);
            CharacterEntity.GrapplingHookState.SetHookReadyState();
        }
    }
}
