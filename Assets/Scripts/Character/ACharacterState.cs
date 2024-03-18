namespace Character
{
    public abstract class ACharacterState
    {
        protected readonly CharacterEntity CharacterEntity;

        protected ACharacterState(CharacterEntity characterEntity)
        {
            CharacterEntity = characterEntity;
        }

        public virtual void Enter() {}
        public virtual void Update() {}
        public virtual void FixedUpdate() {}
        public virtual void Exit() {}
    }
}
