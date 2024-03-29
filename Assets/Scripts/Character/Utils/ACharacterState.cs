using UnityEngine;

namespace Character.Utils
{
    public abstract class ACharacterState
    {
        protected readonly CharacterEntity CharacterEntity;
        protected readonly Transform Transform;

        protected ACharacterState(CharacterEntity characterEntity)
        {
            CharacterEntity = characterEntity;
            Transform = characterEntity.Character.transform;
        }

        public virtual void Enter() {}
        public virtual void Update() {}
        public virtual void FixedUpdate() {}
        public virtual void Exit() {}
    }
}
