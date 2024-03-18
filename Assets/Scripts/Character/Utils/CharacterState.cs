namespace Character.Utils
{
    public abstract class CharacterState
    {
        public virtual void Enter() {}
        public virtual void Update() {}
        public virtual void FixedUpdate() {}
        public virtual void Exit() {}
    }
}
