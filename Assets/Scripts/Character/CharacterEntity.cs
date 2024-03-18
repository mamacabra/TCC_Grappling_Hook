using Character.GrapplingHook;
using UnityEngine;

namespace Character
{
    public struct CharacterEntity
    {
        public CharacterController CharacterController;
        public CharacterInput CharacterInput;
        public CharacterState CharacterState;
        public CharacterUI CharacterUI;
        public GrapplingHookWeapon GrapplingHookWeapon;
    }
}
