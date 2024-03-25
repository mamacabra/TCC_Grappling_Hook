using Character.GrapplingHook;
using UnityEngine;

namespace Character
{
    public struct CharacterEntity
    {
        public bool IsDebug;
        public Rigidbody CharacterRigidbody;
        public CharacterInput CharacterInput;
        public CharacterState CharacterState;
        public CharacterUI CharacterUI;
        public GrapplingHookWeapon GrapplingHookWeapon;
    }
}
