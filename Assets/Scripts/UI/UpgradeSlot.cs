using Inventory;
using Shop;
using Systems;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UpgradeSlot : MonoBehaviour
    {
        private UpgradeManager upgradeManager;
        private ScriptableObject item;
        
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI descriptionText;
        [SerializeField] private Button selectButton;
        
        public void Setup(StatModifierItem item, UpgradeManager manager)
        {
            this.item = item;
            upgradeManager = manager;

            icon.sprite = item.icon;
            nameText.text = item.name;
            descriptionText.text = item.description;
            
            selectButton.interactable = !upgradeManager.HasSelectedUpgrade;
            selectButton.onClick.AddListener(() => upgradeManager.SelectUpgrade(item));
        }
        
        public void Setup(WeaponItem weapon, UpgradeManager manager)
        {
            item = weapon;
            upgradeManager = manager;
            
            icon.sprite = weapon.icon;
            nameText.text = weapon.weaponName;
            descriptionText.text = weapon.description;
            
            selectButton.interactable = !upgradeManager.HasSelectedUpgrade;
            selectButton.onClick.AddListener(() => upgradeManager.SelectUpgrade(weapon));
        }
        
        public bool MatchesItem(ScriptableObject item) => this.item == item;

        public void MarkAsSelected()
        {
            selectButton.interactable = false;
            selectButton.GetComponentInChildren<TextMeshProUGUI>().text = "Selected";
        }
    }
}