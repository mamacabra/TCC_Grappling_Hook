using UnityEngine;
using System.Collections.Generic;

namespace TrapSystem_Scripts.ModifierSystem
{
    public interface IModifyable {
        List<AModifier> Modifiers { get; }
        void AddModifier(AModifier modifier) {
            if (Modifiers.Contains(modifier)) return;
            Modifiers.Add(modifier);
            modifier.Enter();
        }
        void RemoveModifier(AModifier modifier){
            if (!Modifiers.Contains(modifier)) return;
            Modifiers.Remove(modifier);
            modifier.Exit();
        }
    }
}