using System;
using System.Collections.Generic;
using Inventory;
using Sirenix.Serialization;
using Systems;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Shop
{
    public class ShopManager : MonoBehaviour
    {
        private List<StatModifierItem> currentItemChoices = new();
        private List<WeaponItem> currentWeaponChoices = new();
        
        [SerializeField] private ShopUI shopUI;
        [SerializeField] private InventoryManager inventoryManager;

        [SerializeField] private int itemsPerShop = 4;
        [OdinSerialize, SerializeField] private List<StatModifierItem> possibleItems = new();
        [OdinSerialize, SerializeField] private List<WeaponItem> possibleWeapons = new();

        private void OnEnable()
        {
            GameManager.Instance.OnStoreOpen += OpenShop;
        }

        private void OnDisable()
        {
            GameManager.Instance.OnStoreOpen -= OpenShop;
        }

        public void CloseShop()
        {
            shopUI.Hide();
            Time.timeScale = 1f;
        }

        public void BuyItem(StatModifierItem item, int price)
        {
            if (GameManager.Instance.Coins < price) return;
            
            GameManager.Instance.SpendCoins(price);
            inventoryManager.EquipItem(item);
            shopUI.MarkAsPurchased(item);
        }
        
        public void BuyWeapon(WeaponItem weapon, int price)
        {
            if (GameManager.Instance.Coins < price) return;
            
            GameManager.Instance.SpendCoins(price);
            inventoryManager.EquipWeapon(weapon);
            shopUI.MarkAsPurchased(weapon);
        }
        
        public void RerollShop()
        {
            currentItemChoices = DrawRandomItems(possibleItems, itemsPerShop);
            currentWeaponChoices = DrawRandomItems(possibleWeapons, itemsPerShop);
            
            shopUI.Show(currentItemChoices, currentWeaponChoices, this);
        }
        
        public List<StatModifierItem> DrawRandomItems(int count) => DrawRandomItems(possibleItems, count);
        
        public List<WeaponItem> DrawRandomWeapons(int count) => DrawRandomItems(possibleWeapons, count);
        
        private void OpenShop()
        {
            OpenShop(GameManager.Instance.CurrentRound);
        }

        private void OpenShop(int round)
        {
            currentItemChoices = DrawRandomItems(possibleItems, itemsPerShop);
            currentWeaponChoices = DrawRandomItems(possibleWeapons, itemsPerShop);
            
            shopUI.Show(currentItemChoices, currentWeaponChoices, this);
            Time.timeScale = 0f;
        }

        private List<T> DrawRandomItems<T>(List<T> pool, int count)
        {
            var result = new List<T>();
            var poolCopy = new List<T>(pool);

            for (var i = 0; i < count && poolCopy.Count > 0; i++)
            {
                var idx = Random.Range(0, poolCopy.Count);
                result.Add(poolCopy[idx]);
                poolCopy.RemoveAt(idx);
            }
            
            return result;
        }
    }
}