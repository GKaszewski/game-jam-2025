using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class CharacterAttributes
    {
        private float lastLevel = 1f;

        [OdinSerialize, DictionaryDrawerSettings(KeyLabel = "Stat", ValueLabel = "Value", DisplayMode = DictionaryDisplayOptions.OneLine)]
        private Dictionary<Attribute, AttributeData<float>> attributes = new();
        
        public event Action OnLevelUp;

        public CharacterAttributes()
        {
            foreach (Attribute attr in Enum.GetValues(typeof(Attribute)))
            {
                if (!attributes.ContainsKey(attr))
                    attributes[attr] = new AttributeData<float>();
            }
        }
        
        public float Get(Attribute attr) => attributes[attr].Value;
        
        public void Set(Attribute attr, float value) => attributes[attr].Set(value);
        
        public void Modify(Attribute attr, float delta) => attributes[attr].Set(attributes[attr].Value + delta);
        
        public void Subscribe(Attribute attr, Action<float> listener) => attributes[attr].OnChanged += listener;
        
        public void Unsubscribe(Attribute attr, Action<float> listener) => attributes[attr].OnChanged -= listener;

        public void Reset()
        {
            Set(Attribute.Health, Get(Attribute.MaxHealth));
            Set(Attribute.MoveSpeed, 5f);
            Set(Attribute.Luck, 0f);
            Set(Attribute.Armor, 0f);
            Set(Attribute.Damage, 1f);
            Set(Attribute.RangedDamage, 1f);
            Set(Attribute.MeleeDamage, 1f);
            Set(Attribute.AttackRange, 1f);
            Set(Attribute.AttackSpeed, 1f);
            Set(Attribute.Level, 1f);
            Set(Attribute.Experience, 0f);
            Set(Attribute.BaseExperienceToNextLevel, 100f);
        }
        
        /*
         * health: 10
           maxHealth: 10
           moveSpeed: 2
           luck: 0
           armor: 0
           level: 1
           experience: 0
           baseExperienceToLevelUp: 100
           damage: 1
           rangedDamage: 1
           meleeDamage: 1
           attackRange: 1
           attackSpeed: 1
           
           basic enemy btw
         */

        public void Init()
        {
            foreach (Attribute attr in Enum.GetValues(typeof(Attribute)))
            {
                if (!attributes.ContainsKey(attr))
                    attributes[attr] = new AttributeData<float>();
            }

            lastLevel = Get(Attribute.Level);

            Subscribe(Attribute.Experience, OnExperienceChanged);
        }

        private int ExperienceToNextLevel()
        {
            return (int)(Get(Attribute.BaseExperienceToNextLevel) * Math.Pow(Get(Attribute.Level), 2));
        }
        
        private void OnExperienceChanged(float newExp)
        {
            var xpToNext = ExperienceToNextLevel();

            if (newExp >= xpToNext)
            {
                Modify(Attribute.Level, 1f);
                Set(Attribute.Experience, newExp - xpToNext);
            }
            else if (newExp < 0)
            {
                Set(Attribute.Experience, 0f);
            }

            var currentLevel = Get(Attribute.Level);
            if (currentLevel > lastLevel)
            {
                lastLevel = currentLevel;
                OnLevelUp?.Invoke();
            }
        }
    }
}