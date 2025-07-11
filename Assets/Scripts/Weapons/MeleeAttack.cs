using System;
using Data;
using Interfaces;
using Systems;
using UnityEngine;

namespace Weapons
{
    public class MeleeAttack : Weapon, IWeapon
    {
        [SerializeField] private float range = 1f;
        [SerializeField] private LayerMask targetMask;
        
        public override void Fire()
        {
            var hits = Physics2D.OverlapCircleAll(transform.position, range, targetMask);
            foreach (var hit in hits)
            {
                hit.TryGetComponent<Health>(out var health);
                if (hit.gameObject == character.gameObject) continue;
                
                var damage = weaponStats.Damage + character.attributes.Damage * character.attributes.MeleeDamage;
                health.TakeDamage(damage);
            }
        }
    }
}