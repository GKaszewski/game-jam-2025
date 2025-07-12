using System;
using Data;
using Interfaces;
using KBCore.Refs;
using Systems;
using UnityEngine;

namespace Weapons
{
    public class Projectile : MonoBehaviour, IDamageInflector, IDamageInflectorSetup
    {
        [Self, SerializeField] private Rigidbody2D rb;
        [SerializeField] private float speed = 10f;
        [SerializeField] private float lifeTime = 5f;
        
        public float Damage { get; private set; }
        public GameObject Owner { get; private set; }
        public DamageType Type => DamageType.Ranged;
        
        public void Setup(Character attacker, float damage, WeaponStats weaponStats = null)
        {
            Damage = damage;
            Owner = attacker.gameObject;
        }
        
        private void Start()
        {
            Destroy(gameObject, lifeTime);
        }

        private void FixedUpdate()
        {
            var direction = transform.up.normalized;
            var movement = direction * (speed * Time.fixedDeltaTime);
            rb.MovePosition(rb.position + (Vector2)movement);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            other.TryGetComponent<Health>(out var health);
            if (other.gameObject == Owner) return;
            
            health?.TakeDamage(Damage);
            Destroy(gameObject);
        }
    }
}