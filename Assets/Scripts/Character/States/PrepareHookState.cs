using UnityEngine;
using Character.GrapplingHook;
using Character.Utils;

namespace Character.States
{
    public class PrepareHookState : CharacterState
    {
        private float _countDown;
        private const float CountDownStep = 0.4f;
        private readonly Character _character;

        public PrepareHookState(Character character)
        {
            _character = character;
        }

        public override void FixedUpdate() {
            _countDown += Time.fixedDeltaTime;

            if (_countDown > CountDownStep && _character.CharacterEntity.GrapplingHookWeapon.Force < GrapplingHookWeapon.MaxGrapplingHookForce)
            {
                _countDown = 0f;
                _character.CharacterEntity.GrapplingHookWeapon.IncreaseHookForce();
            }
        }
    }
}
