using System;

namespace TrapSystem_Scripts.ModifierSystem
{
    public abstract class AModifier {
        public abstract float GetValue(int index);
    }
    public class AccelerationModifier : AModifier {
        public float acceleration; // index 0
        public float maxSpeed; // index 1

        public override float GetValue(int index)
        {
            return index switch
            {
                0 => acceleration,
                1 => maxSpeed,
                _ => 0
            };
        }
    }
}