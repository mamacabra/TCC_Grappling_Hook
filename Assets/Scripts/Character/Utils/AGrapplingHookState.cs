namespace Character.Utils
{
    public abstract class AGrapplingHookState
    {
        protected readonly CharacterEntity CharacterEntity;

        protected AGrapplingHookState(CharacterEntity characterEntity)
        {
            CharacterEntity = characterEntity;
        }

        public virtual void Enter() {}
        public virtual void Update() {}
        public virtual void FixedUpdate() {}
        public virtual void Exit() {}
    }
}