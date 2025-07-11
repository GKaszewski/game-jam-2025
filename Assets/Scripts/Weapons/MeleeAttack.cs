using System;
using Data;
using Interfaces;
using Systems;
using UnityEngine;

namespace Weapons
{
    public class MeleeAttack : Weapon
    {
        [SerializeField] private LayerMask targetMask;
        
        public override void Fire()
        {
            var finalRange = GetFinalRange();
            var hits = Physics2D.OverlapCircleAll(transform.position, finalRange, targetMask);
            foreach (var hit in hits)
            {
                hit.TryGetComponent<Health>(out var health);
                if (hit.gameObject == character.gameObject) continue;
                
                var damage = GetFinalDamage();
                health?.TakeDamage(damage);
            }
        }
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, GetFinalRange());
        }
    }
}