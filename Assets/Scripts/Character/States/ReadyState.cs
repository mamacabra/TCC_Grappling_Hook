using Character.Utils;
using UnityEngine;

namespace Character.States
{
    public class ReadyState : ACharacterState
    {
        public ReadyState(CharacterEntity characterEntity) : base(characterEntity) {}
        private float timer = 0;
        public override void Enter()
        {
            timer = 0.75f; // Wait to camera move back to play anims
            Transform.LookAt(-Transform.forward, Transform.up); // Made character look at camera direction
        }

        public override void Update()
        {
            if (timer > 0.0f){
                timer -= Time.deltaTime;
                if (timer <= 0.0f)
                    CharacterEntity.CharacterMesh.animator?.SetTrigger("Intro");
            }
        }
    }
}
