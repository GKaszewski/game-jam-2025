using System;
using Data;
using Interfaces;
using Systems;
using UnityEngine;
using Attribute = Data.Attribute;

namespace Modifiers
{
    [Serializable]
    public class HealOnKillModifier : IStatModifier, IOnKillEffect
    {
        public float value;
        public string Description => $"+{value} Health on Kill";
        
        public void Apply(CharacterAttributes attributes) { }

        public void Remove(CharacterAttributes attributes) { }
        
        public void OnKill(GameObject killer, GameObject victim)
        {
            killer.TryGetComponent(out Character character);
            character?.attributes.Modify(Attribute.Health, value);
        }
    }
}