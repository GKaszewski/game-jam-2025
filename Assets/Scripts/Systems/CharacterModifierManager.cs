using System;
using System.Collections.Generic;
using Data;
using Interfaces;
using KBCore.Refs;
using Sirenix.Serialization;
using UnityEngine;

namespace Systems
{
    public class CharacterModifierManager : MonoBehaviour
    {
        [OdinSerialize] private List<IStatModifier> activeModifiers = new();
        [OdinSerialize] private List<IOnKillEffect> onKillEffects = new();
        
        [SerializeField, Self] private Character character;

        private void OnEnable()
        {
            EnemyDeathBehavior.OnAnyEnemyKilled += HandleEnemyKilled;
        }

        private void OnDisable()
        {
            EnemyDeathBehavior.OnAnyEnemyKilled -= HandleEnemyKilled;
        }

        public void EquipItem(IStatModifier modifier)
        {
            activeModifiers.Add(modifier);
            modifier.Apply(character.attributes);
            
            if (modifier is IOnKillEffect onKillEffect)
            {
                onKillEffects.Add(onKillEffect);
            }
        }
        
        public void UnequipItem(IStatModifier modifier)
        {
            if (!activeModifiers.Remove(modifier)) return;
            
            modifier.Remove(character.attributes);
                
            if (modifier is IOnKillEffect onKillEffect)
            {
                onKillEffects.Remove(onKillEffect);
            }
        }

        private void HandleEnemyKilled(GameObject killer, GameObject victim)
        {
            if (killer != gameObject) return;

            foreach (var effect in onKillEffects)
            {
                effect.OnKill(killer, victim);
            }
        }
    }
}