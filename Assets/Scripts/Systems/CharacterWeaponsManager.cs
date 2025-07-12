using System;
using System.Collections.Generic;
using Data;
using Inventory;
using KBCore.Refs;
using Sirenix.Serialization;
using UnityEngine;
using Weapons;

namespace Systems
{
    public class CharacterWeaponsManager : MonoBehaviour
    {
        [OdinSerialize] private List<Weapon> equippedWeapons = new();
        
        [SerializeField] private IReadOnlyList<Weapon> EquippedWeapons => equippedWeapons.AsReadOnly();
        [SerializeField, Self] private Character character;
        
        public event Action<Weapon> WeaponEquipped;
        public event Action<Weapon> WeaponUnequipped;

        public Weapon EquipWeapon(GameObject weaponPrefab)
        {
            var weaponObject = Instantiate(weaponPrefab, transform);
            if (weaponObject.TryGetComponent(out Weapon weapon))
            {
                weapon.character = character;
                equippedWeapons.Add(weapon);
                weapon.enabled = true;
                WeaponEquipped?.Invoke(weapon);
                return weapon;
            }

            Destroy(weaponObject);
            return null;
        }
        
        public void UnequipWeapon(Weapon weapon, bool destroy = true)
        {
            if (!equippedWeapons.Remove(weapon)) return;
            weapon.enabled = false;
            WeaponUnequipped?.Invoke(weapon);

            if (!destroy) return;
            Destroy(weapon.gameObject);
        }
        
        public void DisableAllWeapons()
        {
            foreach (var weapon in equippedWeapons)
            {
                weapon.enabled = false;
                WeaponUnequipped?.Invoke(weapon);
            }
        }
        
        public void EnableAllWeapons()
        {
            foreach (var weapon in equippedWeapons)
            {
                weapon.enabled = true;
                WeaponEquipped?.Invoke(weapon);
            }
        }
        
        public void ClearWeapons()
        {
            foreach (var weapon in equippedWeapons)
            {
                WeaponUnequipped?.Invoke(weapon);
                Destroy(weapon.gameObject);
            }
            equippedWeapons.Clear();
        }
    }
}