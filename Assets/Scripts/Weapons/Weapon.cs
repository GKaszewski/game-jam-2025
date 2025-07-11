using System;
using Data;
using Interfaces;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Weapons
{
    public abstract class Weapon : MonoBehaviour, IWeapon
    {
        private float timer;
        
        [OdinSerialize, InlineProperty] public WeaponStats weaponStats = new();
        public Character character;

        private void Update()
        {
            timer -= Time.deltaTime;

            if (!(timer <= 0f)) return;
            
            Fire();
            timer = 1f / GetFinalAttackSpeed();
        }
        
        private float GetFinalAttackSpeed()
        {
            return character.attributes.AttackSpeed * weaponStats.attackSpeed;
        }

        protected float GetFinalDamage()
        {
            return weaponStats.damage + character.attributes.Damage * 
                   (weaponStats.damageType == DamageType.Melee ? character.attributes.MeleeDamage : character.attributes.RangedDamage);
        }

        protected float GetFinalRange()
        {
            return weaponStats.range * character.attributes.AttackRange;
        }

        public abstract void Fire();
    }
}