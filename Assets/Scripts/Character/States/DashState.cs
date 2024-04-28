using Character.Utils;
using UnityEngine;

namespace Character.States
{
    public class DashState : ACharacterState
    {
        private float _countDown;
        private const float DashSpeed = 70f;
        private const float DashDuration = 0.15f;

        private bool _hasHit = true;
        private const float RaycastDistance = 2.5f;
        private Color RaycastColor => _hasHit ? Color.red : Color.green;

        public DashState(CharacterEntity characterEntity) : base(characterEntity) {}

        public override void Enter()
        {
            CharacterEntity.Character.UseDash();
        }

        public override void Update()
        {
            var direction = CharacterEntity.CharacterInput.transform.forward;

            if (_hasHit == false)
            {
                CharacterEntity.Rigidbody.MovePosition(CharacterEntity.Rigidbody.transform.position + direction * (DashSpeed * Time.deltaTime));
                CharacterEntity.CharacterMesh.animator?.SetFloat("Speed", 1);
            }

            if (_countDown > DashDuration)
            {
                Exit();
            }
        }

        public override void FixedUpdate()
        {
            _countDown += Time.fixedDeltaTime;
            RaycastTest();
        }

        public override void Exit()
        {
            CharacterEntity.CharacterState.SetWalkState();
            CharacterEntity.Character.StartDashCountDown();
        }

        private void RaycastTest()
        {
            var direction = Transform.forward;
            var position = Transform.position;
            var origin = new Vector3(position.x, 1f, position.z) + direction;

            Physics.Raycast(origin, direction, out var hit, RaycastDistance);
            Debug.DrawRay(origin, direction * RaycastDistance, RaycastColor);

            if (hit.collider)
            {
                _hasHit = hit.collider.CompareTag(Const.Tags.Wall) || hit.collider.CompareTag(Const.Tags.Object);
            }
            else
            {
                _hasHit = false;
            }
        }
    }
}
