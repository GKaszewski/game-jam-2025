using System;
using System.Linq;
using Inventory;
using KBCore.Refs;
using UnityEngine;
using Weapons;

namespace Systems
{
    public class InventoryManager : MonoBehaviour
    {
        [SerializeField, Self] private Inventory.Inventory inventory;
        [SerializeField, Self] private CharacterModifierManager characterModifierManager;
        [SerializeField, Self] private CharacterWeaponsManager characterWeaponsManager;
        
        public event Action<StatModifierItem> ItemEquipped;
        public event Action<StatModifierItem> ItemUnequipped;

        private void Start()
        {
            if (inventory == null)
            {
                Debug.LogError("Inventory is not assigned in InventoryManager.");
                return;
            }

            if (characterModifierManager == null)
            {
                Debug.LogError("CharacterModifierManager is not assigned in InventoryManager.");
                return;
            }

            if (characterWeaponsManager == null)
            {
                Debug.LogError("CharacterWeaponsManager is not assigned in InventoryManager.");
                return;
            }

            EquipAllItems();
            EquipAllWeapons();
        }

        public void EquipItem(StatModifierItem item)
        {
            inventory.AddItem(item);
            foreach (var cure in item.cures) characterModifierManager.EquipItem(cure);
            foreach (var curse in item.curses) characterModifierManager.EquipItem(curse);
            ItemEquipped?.Invoke(item);
        }

        public void UnequipItem(StatModifierItem item)
        {
            if (inventory.ItemSlots.All(slot => slot.item != item)) return;
            inventory.RemoveItem(item);
            
            foreach (var cure in item.cures) characterModifierManager.UnequipItem(cure);
            foreach (var curse in item.curses) characterModifierManager.UnequipItem(curse);
            ItemUnequipped?.Invoke(item);
        }

        public void EquipWeapon(WeaponItem weaponItem)
        {
            inventory.AddWeapon(weaponItem);
            characterWeaponsManager.EquipWeapon(weaponItem.prefab);
        }
        
        public void UnequipWeapon(WeaponItem weaponItem)
        {
            if (inventory.WeaponSlots.All(slot => slot.item != weaponItem)) return;
            inventory.RemoveWeapon(weaponItem);
            weaponItem.prefab.TryGetComponent(out Weapon weapon);
            characterWeaponsManager.UnequipWeapon(weapon);
        }
        
        private void EquipAllWeapons()
        {
            foreach (var slot in inventory.WeaponSlots)
            {
               EquipWeapon(slot.item as WeaponItem);
            }
        }
        
        private void EquipAllItems()
        {
            foreach (var slot in inventory.ItemSlots)
            {
                EquipItem(slot.item as StatModifierItem);
            }
        }
    }
}