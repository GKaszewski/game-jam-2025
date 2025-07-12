using Data;
using Interfaces;
using KBCore.Refs;
using Systems;
using UnityEngine;

namespace Weapons
{
    public class ExplosiveProjectile : MonoBehaviour, IDamageInflector, IDamageInflectorSetup
    {
        [Self, SerializeField] private Rigidbody2D rb;
        [SerializeField] private float speed = 10f;
        [SerializeField] private float lifeTime = 5f;
        [SerializeField] private WeaponStats stats;
        
        public float Damage { get; private set; }
        public GameObject Owner { get; private set; }
        public DamageType Type => DamageType.Ranged;
        
        public void Setup(Character attacker, float damage, WeaponStats weaponStats = null)
        {
            Damage = damage;
            Owner = attacker.gameObject;
            if (weaponStats != null)
            {
                stats = weaponStats;
            }
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
            Explode();
        }

        private void Explode()
        {
            var hitColliders = Physics2D.OverlapCircleAll(transform.position, stats.range);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.gameObject == Owner) continue;

                hitCollider.TryGetComponent<Health>(out var health);
                health?.TakeDamage(Damage, Owner);
            }
            Destroy(gameObject);
        }
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, stats.range);
        }
    }
}