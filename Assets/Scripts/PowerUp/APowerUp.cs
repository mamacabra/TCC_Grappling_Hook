using Character;

namespace PowerUp
{
    public abstract class APowerUp
    {
        protected CharacterEntity CharacterEntity;

        protected APowerUp(CharacterEntity entity)
        {
            CharacterEntity = entity;
        }

        public virtual void OnCatch() {}

        public virtual void OnDrop() {}
    }
}
