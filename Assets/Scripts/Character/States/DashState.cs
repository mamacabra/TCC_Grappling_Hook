using Character.Utils;
using UnityEngine;

namespace Character.States
{
    public class DashState : ACharacterState
    {
        private float _countDown;
        private const float DashDistance = 2f;
        private const float DashDuration = 0.1f;

        public DashState(CharacterEntity characterEntity) : base(characterEntity) {}

        public override void Update()
        {
            var direction = CharacterEntity.CharacterInput.transform.forward;

            if (CharacterEntity.CharacterRaycast.HasHit == false)
            {
                CharacterEntity.CharacterRigidbody.MovePosition(CharacterEntity.CharacterRigidbody.transform.position + direction * DashDistance / DashDuration * Time.deltaTime);
            }

            if (_countDown > DashDuration)
            {
                Exit();
            }
        }

        public override void FixedUpdate()
        {
            _countDown += Time.fixedDeltaTime;
        }

        public override void Exit()
        {
            CharacterEntity.CharacterState.SetWalkState();
        }
    }
}
