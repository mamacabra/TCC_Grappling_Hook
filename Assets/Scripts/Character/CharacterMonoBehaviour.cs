using UnityEngine;

namespace Character
{
    public abstract class CharacterMonoBehaviour : MonoBehaviour
    {
        protected CharacterEntity CharacterEntity;

        public void Setup(CharacterEntity entity)
        {
            CharacterEntity = entity;
        }
    }
}
