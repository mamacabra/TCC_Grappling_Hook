using Character.Utils;

namespace Character
{
    public class Character : ACharacterMonoBehaviour
    {
        public new CharacterEntity CharacterEntity { get; private set; }
        public int Id;
        public new void Setup(CharacterEntity entity)
        {
            CharacterEntity = entity;
        }
    }
}
