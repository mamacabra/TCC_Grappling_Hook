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

        public override void FixedUpdate() {
            _countDown += Time.fixedDeltaTime;

            if (_countDown > CountDownStep && CharacterEntity.GrapplingHookWeapon.Force < GrapplingHookWeapon.MaxGrapplingHookForce)
            {
                _countDown = 0f;
                CharacterEntity.GrapplingHookWeapon.IncreaseHookForce();
            }
        }

        public override void Update()
        {
            var movementInput = CharacterEntity.CharacterInput.movementInput;
            var direction = new Vector3(movementInput.x, 0, movementInput.y);

            direction += CharacterEntity.CharacterRigidbody.transform.position;
            CharacterEntity.CharacterRigidbody.MovePosition(direction * (MovementSpeed * Time.deltaTime));

            if (direction != Vector3.zero)
            {
                var transform = CharacterEntity.CharacterInput.transform;
                var toRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, Time.deltaTime * RotationSpeed);
            }
        }
    }
}
