using UnityEngine;
using System.Collections.Generic;

namespace TrapSystem_Scripts.ModifierSystem
{
    public interface IModifyable {
        List<AModifier> Modifiers { get; }
        void AddModifier(AModifier modifier) {
            if (!Modifiers.Contains(modifier)) Modifiers.Add(modifier);
        }
        void RemoveModifier(AModifier modifier){
            if (Modifiers.Contains(modifier)) Modifiers.Remove(modifier);
        }

        public bool TryGetModifier<T>(out T result) where T : AModifier
        {
            result = null;
            foreach (AModifier modifier in Modifiers)
            {
                if (modifier is T)
                {
                    result = modifier as T;
                    return true;
                }
            }
            return false; // Return a default value if the modifier is not found
        }
    }
}