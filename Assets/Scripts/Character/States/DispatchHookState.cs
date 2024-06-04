using Character.Utils;
using UnityEngine;

namespace Character.States
{
    public class DispatchHookState : ACharacterState
    {
        private const float RaycastDistance = 2f;

        public DispatchHookState(CharacterEntity characterEntity) : base(characterEntity) {}

        public override void Enter()
        {
            var origin = new Vector3(Transform.position.x, 1f, Transform.position.z);
            var direction = Transform.forward;

            Physics.Raycast(origin, direction, out var hit, RaycastDistance);
            if (hit.collider) CharacterEntity.CharacterState.SetWalkState();
            else CharacterEntity.GrapplingHookState.SetHookDispatchState();
        }
    }
}
