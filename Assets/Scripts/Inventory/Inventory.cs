using System.Collections.Generic;
using Sirenix.Serialization;
using UnityEngine;

namespace Inventory
{
    public class Inventory : MonoBehaviour
    {
        [OdinSerialize, SerializeField] private List<InventorySlot> weaponSlots = new();
        [OdinSerialize, SerializeField] private List<InventorySlot> itemSlots = new();
        
        public IReadOnlyList<InventorySlot> WeaponSlots => weaponSlots.AsReadOnly();
        public IReadOnlyList<InventorySlot> ItemSlots => itemSlots.AsReadOnly();
        
        public void AddWeapon(WeaponItem weaponItem)
        {
            foreach (var slot in weaponSlots)
            {
                if (slot.item == weaponItem) return;
            }
            
            weaponSlots.Add(new InventorySlot { item = weaponItem, quantity = 1 });
        }
        
        public void AddItem(ScriptableObject item)
        {
            foreach (var slot in itemSlots)
            {
                if (slot.item != item) continue;
                
                slot.quantity++;
                return;
            }
            
            itemSlots.Add(new InventorySlot { item = item, quantity = 1 });
        }
        
        public void RemoveWeapon(WeaponItem weaponItem)
        {
            for (var i = 0; i < weaponSlots.Count; i++)
            {
                if (weaponSlots[i].item != weaponItem) continue;
                
                if (--weaponSlots[i].quantity <= 0)
                {
                    weaponSlots.RemoveAt(i);
                }
                return;
            }
        }
        
        public void RemoveItem(ScriptableObject item)
        {
            for (var i = 0; i < itemSlots.Count; i++)
            {
                if (itemSlots[i].item != item) continue;
                
                if (--itemSlots[i].quantity <= 0)
                {
                    itemSlots.RemoveAt(i);
                }
                return;
            }
        }
    }
}