using Interfaces;
using UnityEngine;

namespace Weapons
{
    public class AutoWeapon : Weapon
    {
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private Transform firePoint;
        
        public override void Fire()
        {
            var projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            projectile.TryGetComponent<IDamageInflectorSetup>(out var inflector);
            
            var finalDamage = GetFinalDamage();
            inflector?.Setup(character, finalDamage, weaponStats);
        }
    }
}