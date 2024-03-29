using Character.Utils;
using UnityEngine;

namespace Character.States
{
    public class HookedState : ACharacterState
    {
        private const float MinDistanceToDrop = 1f;
        private const float MovementSpeed = 20f;
        private readonly Vector3 _enemyPosition;

        public HookedState(CharacterEntity characterEntity, Vector3 enemyPosition) : base(characterEntity)
        {
            _enemyPosition = enemyPosition;
        }

        public override void FixedUpdate()
        {
            var characterPosition = CharacterEntity.CharacterState.transform.position;
            var direction = (_enemyPosition - characterPosition).normalized;

            Debug.Log("characterPosition " + characterPosition.ToString());
            Debug.Log( "_enemyPosition " + _enemyPosition.ToString());
            Debug.Log("direction " + direction.ToString());

            CharacterEntity.CharacterRigidbody.MovePosition(CharacterEntity.CharacterRigidbody.transform.position + direction * (Time.fixedDeltaTime * MovementSpeed));

            var enemyDistance = Vector3.Distance(characterPosition, _enemyPosition);
            Debug.Log("enemyDistance " + enemyDistance.ToString());

            if (enemyDistance <= MinDistanceToDrop)
                CharacterEntity.CharacterState.SetWalkState();
        }
    }
}
