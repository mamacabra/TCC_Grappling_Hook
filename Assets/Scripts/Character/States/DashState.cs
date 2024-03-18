using UnityEngine;
using Character.Utils;

namespace Character.States
{
    public class DashState : CharacterState
    {
        private readonly CharacterEntity _characterEntity;

        private float _countDown;
        private const float DashDistance = 9f;
        private const float DashDuration = 0.1f;

        public DashState(CharacterEntity characterEntity)
        {
            _characterEntity = characterEntity;
        }

        public override void Update()
        {
            var direction = _characterEntity.CharacterInput.transform.forward;
            _characterEntity.CharacterController.Move(direction * DashDistance / DashDuration * Time.deltaTime);

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
            _characterEntity.Character.SetWalkState();
        }
    }
}
