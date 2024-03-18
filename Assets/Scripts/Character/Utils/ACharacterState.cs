namespace Character.Utils
{
    public abstract class ACharacterState
    {
        public virtual void Enter() {}
        public virtual void Update() {}
        public virtual void FixedUpdate() {}
        public virtual void Exit() {}
    }
}
