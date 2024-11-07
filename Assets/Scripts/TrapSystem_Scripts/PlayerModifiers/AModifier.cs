using System;
using System.Collections;
using UnityEngine;

namespace TrapSystem_Scripts.ModifierSystem
{
    public abstract class AModifier {
        public abstract void Enter();
        public abstract void Exit();
        public abstract void ApplyModifier();// For any other type of modifier
        public abstract void ApplyModifier(ref Vector3 targetSpeed, ref float acceleration, Vector3 dir);
    }

    public abstract class MovementModifier : AModifier
    {
        public float acceleration;
        public float maxSpeed;
        public override void Enter() { }
        public override void Exit() { }
        public override void ApplyModifier() { }
        public override void ApplyModifier(ref Vector3 targetSpeed, ref float acceleration, Vector3 dir) {
            targetSpeed = dir * maxSpeed;
            acceleration = this.acceleration * Time.deltaTime;
        }
    }
    public class SlideModifier : MovementModifier {
        public override void Exit() {
        }
    }
    public class GlueModifier : MovementModifier {
        public override void Exit() {
        }
    }
}