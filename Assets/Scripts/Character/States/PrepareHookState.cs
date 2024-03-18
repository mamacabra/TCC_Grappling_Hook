using UnityEngine;
using Character.GrapplingHook;
using Character.Utils;

namespace Character.States
{
    public class PrepareHookState : ACharacterState
    {
        private float _countDown;
        private const float CountDownStep = 0.4f;
        private readonly CharacterEntity _characterEntity;

        public PrepareHookState(CharacterEntity characterEntity)
        {
            _characterEntity = characterEntity;
        }

        public override void FixedUpdate() {
            _countDown += Time.fixedDeltaTime;

            if (_countDown > CountDownStep && _characterEntity.GrapplingHookWeapon.Force < GrapplingHookWeapon.MaxGrapplingHookForce)
            {
                _countDown = 0f;
                _characterEntity.GrapplingHookWeapon.IncreaseHookForce();
            }
        }
    }
}
