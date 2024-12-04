using UnityEngine;
using System.Collections.Generic;
using Character;

namespace TrapSystem_Scripts.ModifierSystem
{
    public interface IModifyable {
        List<AModifier> Modifiers { get; }
        void AddModifier(CharacterEntity characterEntity, AModifier modifier) {
            if (Modifiers.Contains(modifier)) return;
            Modifiers.Add(modifier);
            modifier.Enter(characterEntity);
        }
        void RemoveModifier(CharacterEntity characterEntity, AModifier modifier){
            if (!Modifiers.Contains(modifier)) return;
            Modifiers.Remove(modifier);
            modifier.Exit(characterEntity);
        }
    }
}
