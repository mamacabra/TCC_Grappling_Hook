using UnityEngine;
using Character.GrapplingHook;
using Character.Utils;

namespace Character.States
{
    public class PrepareHookState : ACharacterState
    {
        private float _countDown;
        private const float CountDownStep = 0.2f;
        private const float MovementSpeed = 12f;
        private const float RotationSpeed = 800f;

        public PrepareHookState(CharacterEntity characterEntity) : base(characterEntity) {}

        public override void Update()
        {
            var movementInput = CharacterEntity.CharacterInput.movementInput;
            var direction = new Vector3(movementInput.x, 0, movementInput.y);

            CharacterEntity.Rigidbody.MovePosition(CharacterEntity.Rigidbody.transform.position + direction * (MovementSpeed * Time.deltaTime));

            if (direction != Vector3.zero)
            {
                var transform = CharacterEntity.CharacterInput.transform;
                var toRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, Time.deltaTime * RotationSpeed);
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
