using Interfaces;
using UnityEngine;

namespace Weapons
{
    public class AutoWeapon : Weapon
    {
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private Transform firePoint;
        
        public Vector2 Target { get; set; }
        
        public override void Fire()
        {
            var direction = (Target - (Vector2)firePoint.position).normalized;
            firePoint.up = direction;
            Debug.DrawLine(firePoint.position, Target, Color.red, 2f);
            
            var projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            projectile.TryGetComponent<IDamageInflectorSetup>(out var inflector);
            
            var finalDamage = GetFinalDamage();
            inflector?.Setup(character, finalDamage, weaponStats);
        }
    }
}