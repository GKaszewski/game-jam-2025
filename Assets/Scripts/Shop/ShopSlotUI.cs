using Inventory;
using Systems;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Shop
{
    public class ShopSlotUI : MonoBehaviour
    {
        private ScriptableObject item;
        private ShopManager shopManager;
        private int price;
        
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI descriptionText;
        [SerializeField] private TextMeshProUGUI priceText;
        [SerializeField] private Button purchaseButton;
        
        public void Setup(StatModifierItem item, ShopManager manager)
        {
            this.item = item;
            shopManager = manager;
            price = item.price;

            icon.sprite = item.icon;
            nameText.text = item.name;
            descriptionText.text = item.description;
            priceText.text = $"Price: {price}";

            purchaseButton.interactable = GameManager.Instance.Coins >= price;
            purchaseButton.onClick.AddListener(() => shopManager.BuyItem(item, price));
        }
        
        public void Setup(WeaponItem weapon, ShopManager manager)
        {
            item = weapon;
            shopManager = manager;
            price = weapon.price;
            icon.sprite = weapon.icon;
            nameText.text = weapon.weaponName;
            descriptionText.text = weapon.description;
            priceText.text = $"Price: {price}";
            purchaseButton.interactable = GameManager.Instance.Coins >= price;
            purchaseButton.onClick.AddListener(() => shopManager.BuyWeapon(weapon, price));
        }

        public bool MatchesItem(ScriptableObject item) => this.item == item;

        public void MarkAsPurchased()
        {
            purchaseButton.interactable = false;
            purchaseButton.GetComponentInChildren<TextMeshProUGUI>().text = "Purchased";
        }
    }
}