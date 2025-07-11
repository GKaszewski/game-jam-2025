using Interfaces;
using UnityEngine;

namespace Weapons
{
    public class AutoWeapon : Weapon, IWeapon
    {
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private Transform firePoint;
        
        public override void Fire()
        {
            var projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            projectile.TryGetComponent<IDamageInflectorSetup>(out var inflector);

            inflector?.Setup(character);
        }
    }
}