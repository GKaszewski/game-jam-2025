using Inventory;
using UI;
using UnityEngine;

namespace Systems
{
    public class UpgradeManager : MonoBehaviour
    {
        private bool hasSelectedUpgrade;

        [SerializeField] private InventoryManager inventoryManager;
        [SerializeField] private LevelUpHud levelUpHud;

        public bool HasSelectedUpgrade => hasSelectedUpgrade;
        
        public void SetHasSelectedUpgrade(bool value)
        {
            hasSelectedUpgrade = value;
        }
        
        public void SelectUpgrade(StatModifierItem item)
        {
            if (hasSelectedUpgrade) return;

            inventoryManager.EquipItem(item);
            hasSelectedUpgrade = true;
            levelUpHud.MarkAsSelected(item);

        }

        public void SelectUpgrade(WeaponItem weapon)
        {
            if (hasSelectedUpgrade) return;

            inventoryManager.EquipWeapon(weapon);
            hasSelectedUpgrade = true;
            levelUpHud.MarkAsSelected(weapon);
        }
    }
}