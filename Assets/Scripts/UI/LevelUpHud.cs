using System;
using System.Collections.Generic;
using Inventory;
using KBCore.Refs;
using Shop;
using Systems;
using UnityEngine;
using Attribute = Data.Attribute;

namespace UI
{
    public class LevelUpHud : MonoBehaviour
    {
        private float previousLevel = 1f;
        private List<UpgradeSlot> currentSlots = new();
        private List<StatModifierItem> currentItemChoices = new();
        private List<WeaponItem> currentWeaponChoices = new(); 
        
        [SerializeField] private GameObject itemsContainer;
        [SerializeField] private GameObject upgradeSlotPrefab;
        [SerializeField] private int itemsCount = 3;
        [SerializeField] private int weaponsCount = 2;
        [SerializeField, Scene] private ShopManager shopManager;
        [SerializeField, Scene] private UpgradeManager upgradeManager;
        [SerializeField] private AudioClip levelUpSound;

        private void OnEnable()
        {
            Hide();
            GameManager.Instance.Player.attributes.OnLevelUp += OnLevelUp;
        }

        private void OnDisable()
        {
            GameManager.Instance.Player.attributes.OnLevelUp -= OnLevelUp;
        }
        
        private void OnLevelUp()
        {
            if (levelUpSound)
            {
                AudioSource.PlayClipAtPoint(levelUpSound, new Vector3(0f, 0f, 0f));
            }
            
            Show();
        }

        private void Show()
        {
            Time.timeScale = 0f;
            ClearSlots();
            
            currentItemChoices = shopManager.DrawRandomItems(itemsCount);
            currentWeaponChoices = shopManager.DrawRandomWeapons(weaponsCount);
            
            foreach (var item in currentItemChoices)
            {
                var slot = Instantiate(upgradeSlotPrefab, itemsContainer.transform);
                if (slot.TryGetComponent(out UpgradeSlot upgradeSlot))
                {
                    upgradeSlot.Setup(item, upgradeManager);
                    currentSlots.Add(upgradeSlot);
                }
            }
            
            foreach (var weapon in currentWeaponChoices)
            {
                var slot = Instantiate(upgradeSlotPrefab, itemsContainer.transform);
                if (slot.TryGetComponent(out UpgradeSlot upgradeSlot))
                {
                    upgradeSlot.Setup(weapon, upgradeManager);
                    currentSlots.Add(upgradeSlot);
                }
            }
            
            itemsContainer.SetActive(true);
        }

        private void Hide()
        {
            Time.timeScale = 1f;
            itemsContainer.SetActive(false);
            ClearSlots();
        }
        
        private void ClearSlots()
        {
            foreach (Transform child in itemsContainer.transform) Destroy(child.gameObject);
            
            currentSlots.Clear();
            currentItemChoices.Clear();
            currentWeaponChoices.Clear();
            upgradeManager.SetHasSelectedUpgrade(false);
        }

        public void MarkAsSelected(ScriptableObject item)
        {
            foreach (var slot in currentSlots)
            {
                if (slot.MatchesItem(item))
                {
                    slot.MarkAsSelected();
                    Hide();
                    return;
                }
            }
        }
    }
}