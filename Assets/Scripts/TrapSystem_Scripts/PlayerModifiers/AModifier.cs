using Character;
using UnityEngine;

namespace TrapSystem_Scripts.ModifierSystem
{
    public abstract class AModifier {
        public abstract void Enter(CharacterEntity characterEntity);
        public abstract void Exit(CharacterEntity characterEntity);
        public abstract void ApplyModifier(ref Vector3 targetSpeed, ref float acceleration, Vector3 dir);
    }

    public abstract class MovementModifier : AModifier
    {
        public float acceleration;
        public float maxSpeed;
        public override void Enter(CharacterEntity characterEntity) {}
        public override void Exit(CharacterEntity characterEntity) {}
        public override void ApplyModifier(ref Vector3 targetSpeed, ref float acceleration, Vector3 dir)
        {
            targetSpeed = dir * maxSpeed;
            acceleration = this.acceleration * Time.deltaTime;
        }
    }

    public class SlideModifier : MovementModifier {
        public override void Enter(CharacterEntity characterEntity) {
            characterEntity.CharacterVFX?.PlaySlideModifierVFX();
        }

        public override void Exit(CharacterEntity characterEntity) {
            characterEntity.CharacterVFX?.StopSlideModifierVFX();
        }
    }

    public class GlueModifier : MovementModifier {
        public override void Enter(CharacterEntity characterEntity) {
            characterEntity.CharacterVFX?.PlayGlueModifierVFX();
        }

        public override void Exit(CharacterEntity characterEntity) {
            characterEntity.CharacterVFX?.StopGlueModifierVFX();
        }
    }
}
