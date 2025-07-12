using System.Collections.Generic;
using Inventory;
using KBCore.Refs;
using UnityEngine;
using UnityEngine.InputSystem;
using Weapons;

namespace Systems
{
    public class PlayerWeaponTargetSetter : MonoBehaviour
    {
        [SerializeField, Scene] private Camera mainCamera;
        [SerializeField] private List<AutoWeapon> weapons = new();
        [Self, SerializeField] private CharacterWeaponsManager weaponsManager;

        private void OnEnable()
        {
            weaponsManager.WeaponEquipped += OnWeaponEquipped;
            weaponsManager.WeaponUnequipped += OnWeaponUnequipped;
        }

        private void OnDisable()
        {
            weaponsManager.WeaponEquipped -= OnWeaponEquipped;
            weaponsManager.WeaponUnequipped -= OnWeaponUnequipped;
        }

        private void Reset()
        {
            if (!mainCamera) mainCamera = Camera.main;
            if (weapons == null || weapons.Count == 0)
            {
                weapons = new List<AutoWeapon>(GetComponentsInChildren<AutoWeapon>());
            }
        }

        private void Update()
        {
            if (!mainCamera || weapons.Count == 0) return;
            
            var mouseScreen = Mouse.current.position.ReadValue();
            var mouseWorld = mainCamera.ScreenToWorldPoint(mouseScreen);
            mouseWorld.z = 0f;
            
            foreach (var weapon in weapons)
            {
                weapon.Target = mouseWorld;
            }
        }
        
        private void OnWeaponEquipped(Weapon weapon)
        {
            if (!weapon || weapon is not AutoWeapon autoWeapon) return;

            if (autoWeapon && !weapons.Contains(autoWeapon))
            {
                weapons.Add(autoWeapon);
            }
        }
        
        private void OnWeaponUnequipped(Weapon weapon)
        {
            if (!weapon || weapon is not AutoWeapon autoWeapon) return;

            if (autoWeapon && weapons.Contains(autoWeapon))
            {
                weapons.Remove(autoWeapon);
            }
        }
    }
}