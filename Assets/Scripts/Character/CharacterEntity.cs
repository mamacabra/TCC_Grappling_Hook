using Character.GrapplingHook;
using Character.Melee;
using UnityEngine;

namespace Character
{
    public struct CharacterEntity
    {
        public Character Character;
        public BoxCollider CharacterCollider;
        public CharacterInput CharacterInput;
        public CharacterMesh CharacterMesh;
        public CharacterPowerUp CharacterPowerUp;
        public CharacterState CharacterState;
        public CharacterUI CharacterUI;
        public CharacterVFX CharacterVFX;

        public AttackMelee AttackMelee;

        public GrapplingHook.GrapplingHook Hook;
        public GameObject GrapplingHookRope;
        public GameObject GrapplingHookRopeMuzzle;
        public BoxCollider GrapplingHookCollider;
        public GrapplingHookColliderCheck GrapplingHookColliderCheck;
        public GrapplingHookState GrapplingHookState;
        public Transform GrapplingHookTransform;

        public Rigidbody Rigidbody;
    }
}
