using System;
using System.Collections.Generic;
using Inventory;
using Systems;
using TMPro;
using UnityEngine;

namespace Shop
{
    public class ShopUI : MonoBehaviour
    {
        [SerializeField] private GameObject shopPanel;
        [SerializeField] private Transform itemSlotParent;
        [SerializeField] private Transform weaponSlotParent;
        [SerializeField] private ShopSlotUI slotPrefab;
        [SerializeField] private TextMeshProUGUI roundsText;
        [SerializeField] private TextMeshProUGUI coinsText;
        
        private List<ShopSlotUI> currentSlots = new();

        private void OnEnable()
        {
            GameManager.Instance.OnRoundEnd += UpdateRoundText;
        }
        
        private void OnDisable()
        {
            GameManager.Instance.OnRoundEnd -= UpdateRoundText;
        }

        private void Update()
        {
            coinsText.text = $"Coins: {GameManager.Instance.Coins}";
        }

        public void Show(List<StatModifierItem> items, List<WeaponItem> weapons, ShopManager shopManager)
        {
            GameManager.Instance.StoreIsClosed = false;
            UpdateRoundText(GameManager.Instance.CurrentRound);
            
            shopPanel.SetActive(true);
            ClearSlots();

            foreach (var item in items)
            {
                var slot = Instantiate(slotPrefab, itemSlotParent);
                slot.Setup(item, shopManager);
                currentSlots.Add(slot);
            }
            
            foreach (var weapon in weapons)
            {
                var slot = Instantiate(slotPrefab, weaponSlotParent);
                slot.Setup(weapon, shopManager);
                currentSlots.Add(slot);
            }
        }

        public void Hide()
        {
            GameManager.Instance.StoreIsClosed = true;
            shopPanel.SetActive(false);
            ClearSlots();
        }

        public void MarkAsPurchased(ScriptableObject item)
        {
            foreach (var slot in currentSlots)
            {
                if (slot.MatchesItem(item)) slot.MarkAsPurchased();
            }
        }
        
        private void ClearSlots()
        {
            foreach (var slot in currentSlots) Destroy(slot.gameObject);
            
            currentSlots.Clear();
        }
        
        private void UpdateRoundText(int round)
        {
            var nextRound = Mathf.Min(round + 1, GameManager.Instance.MaxRounds);
            roundsText.text = $"Round: {nextRound}/{GameManager.Instance.MaxRounds}";
        }

    }
}