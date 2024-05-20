using UnityEngine;

namespace Character.Utils
{
    public abstract class ACharacterState
    {
        protected readonly CharacterEntity CharacterEntity;
        protected readonly Transform Transform;

        private const float WalkSpeed = 16f;

        protected ACharacterState(CharacterEntity characterEntity)
        {
            CharacterEntity = characterEntity;
            Transform = characterEntity.Character.transform;
        }

        public virtual void Enter() {}
        public virtual void Update() {}
        public virtual void FixedUpdate() {}
        public virtual void Exit() {}

        protected void Walk(float speed = WalkSpeed)
        {
            var direction = CharacterEntity.CharacterInput.MoveDirection;
            Transform.Translate(direction * (speed * Time.deltaTime));

            if (CharacterEntity.CharacterMesh.animator)
            {
                var magnitude = CharacterEntity.CharacterInput.MoveDirection.magnitude;
                CharacterEntity.CharacterMesh.animator.SetFloat("Speed", magnitude);
            }
        }

        protected void LookAt()
        {
            var direction = CharacterEntity.CharacterInput.MoveDirection;
            if (direction != Vector3.zero)
            {
                var lookDirection = CharacterEntity.CharacterInput.LookDirection;
                CharacterEntity.Character.characterBody.LookAt(lookDirection);
            }
        }
    }
}
