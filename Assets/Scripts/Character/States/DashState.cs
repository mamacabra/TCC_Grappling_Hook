using Character.Utils;
using UnityEngine;

namespace Character.States
{
    public class DashState : ACharacterState
    {
        private float _countDown;
        private const float DashSpeed = 70f;
        private const float DashDuration = 0.15f;

        public DashState(CharacterEntity characterEntity) : base(characterEntity) {}

        public override void Update()
        {
            var direction = CharacterEntity.CharacterInput.transform.forward;

            if (CharacterEntity.CharacterRaycast.HasHit == false)
            {
                CharacterEntity.Rigidbody.MovePosition(CharacterEntity.Rigidbody.transform.position + direction * (DashSpeed * Time.deltaTime));
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
