using UnityEngine;
using Character.GrapplingHook;
using Character.Utils;

namespace Character.States
{
    public class PrepareHookState : ACharacterState
    {
        private float _countDown;
        private const float CountDownStep = 0.25f;

        public PrepareHookState(CharacterEntity characterEntity) : base(characterEntity) {}

        public override void FixedUpdate() {
            _countDown += Time.fixedDeltaTime;

            if (_countDown > CountDownStep && CharacterEntity.GrapplingHookWeapon.Force < GrapplingHookWeapon.MaxGrapplingHookForce)
            {
                _countDown = 0f;
                CharacterEntity.GrapplingHookWeapon.IncreaseHookForce();
            }
        }
    }
}
