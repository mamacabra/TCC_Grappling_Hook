using Character.Utils;
using UnityEngine;

namespace Character.States
{
    public class HookedToEnemyState : ACharacterState
    {
        private const float MinDistanceToDrop = 3f;
        private const float MovementSpeed = 20f;
        private readonly Vector3 _enemyPosition;

        public HookedToEnemyState(CharacterEntity characterEntity, Vector3 enemyPosition) : base(characterEntity)
        {
            _enemyPosition = enemyPosition;
            Transform.LookAt(_enemyPosition);
        }

        public override void FixedUpdate()
        {
            var characterPosition = Transform.position;
            var direction = (_enemyPosition - characterPosition).normalized;

            CharacterEntity.Rigidbody.MovePosition(characterPosition + direction * (Time.fixedDeltaTime * MovementSpeed));

            var distance = Vector3.Distance(characterPosition, _enemyPosition);
            if (distance <= MinDistanceToDrop)
            {
                CharacterEntity.CharacterState.SetWalkState();
                // TODO: Chamar função que mata o personagem
            }
        }
    }
}
