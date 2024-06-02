using Character.Utils;
using UnityEngine;

namespace Character.States
{
    public class DispatchHookState : ACharacterState
    {
        private float _hookSpeed = 80;
        private float _hookMaxDistance = 24;

        public DispatchHookState(CharacterEntity characterEntity) : base(characterEntity) {}

        public override void Enter()
        {
            CharacterEntity.GrapplingHookState.SetHookDispatchState();
            CharacterEntity.GrapplingHookWeapon.DispatchHook(CharacterEntity.CharacterState.transform.forward);
        }

        public override void FixedUpdate()
        {
            CharacterEntity.GrapplingHookTransform.Translate(Vector3.forward * (Time.fixedDeltaTime * _hookSpeed));

            // var hookDistance = Vector3.Distance(_hookOriginLocalPosition, hookRigidbody.transform.localPosition);
            // if (_isHookDispatch && hookDistance >= _hookMaxDistance)
            //     CharacterEntity.CharacterState.SetRollbackHookState();
            // else if (_isHookRollback && (hookDistance <= 0.1f || hookRigidbody.transform.localPosition.z <= _hookOriginLocalPosition.z))
            //     CharacterEntity.CharacterState.SetWalkState();
        }
    }
}
