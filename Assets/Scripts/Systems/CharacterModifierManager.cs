using System.Collections.Generic;
using Data;
using Interfaces;
using Sirenix.Serialization;
using UnityEngine;

namespace Systems
{
    public class CharacterModifierManager : MonoBehaviour
    {
        [OdinSerialize] private List<IStatModifier> activeModifiers = new();
        
        [SerializeField] private Character character;
        
        public void EquipItem(IStatModifier modifier)
        {
            activeModifiers.Add(modifier);
            modifier.Apply(character.attributes);
        }
        
        public void UnequipItem(IStatModifier modifier)
        {
            if (activeModifiers.Remove(modifier))
            {
                modifier.Remove(character.attributes);
            }
        }
    }
}