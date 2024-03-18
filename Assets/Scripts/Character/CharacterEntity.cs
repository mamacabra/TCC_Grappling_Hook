using Character.GrapplingHook;
using UnityEngine;

namespace Character
{
    public struct CharacterEntity
    {
        public Character Character;
        public CharacterController CharacterController;
        public CharacterInput CharacterInput;
        public Rigidbody CharacterRigidbody;
        public CharacterUI CharacterUI;
        public GrapplingHookWeapon GrapplingHookWeapon;
    }
}
