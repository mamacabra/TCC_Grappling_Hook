using UnityEngine;
using Character.GrapplingHook;
using Character.Utils;

namespace Character.States
{
    public class PrepareHookState : ACharacterState
    {
        private float _countDown;
        private const float CountDownStep = 0.1f;
        private const float MovementSpeed = 12f;
        private const float RotationSpeed = 800f;

        public PrepareHookState(CharacterEntity characterEntity) : base(characterEntity) {}

        public override void Update()
        {
            var axes = CharacterEntity.CharacterInput.movementInput;
            var moveDirection = new Vector3(axes.x, 0, axes.y);

            CharacterEntity.Rigidbody.MovePosition(CharacterEntity.Rigidbody.transform.position + moveDirection * (MovementSpeed * Time.deltaTime));

            if (moveDirection != Vector3.zero)
            {
                var lookDirection = Transform.position + moveDirection;
                CharacterEntity.Character.characterBody.LookAt(lookDirection);
            }
        }

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
