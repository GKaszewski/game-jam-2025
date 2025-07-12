using System.Collections.Generic;
using Interfaces;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu(menuName = "Game/Item/StatModifierItem")]
    public class StatModifierItem : SerializedScriptableObject
    {
        public string itemName;
        [TextArea] public string description;
        public Sprite icon;
        public int price;
        
        public List<IStatModifier> cures = new();
        public List<IStatModifier> curses = new();
        
        [Button("Build Description")]
        private void BuildDescription()
        {
            var descriptionBuilder = new System.Text.StringBuilder();
            foreach (var modifier in cures)
            {
                if (descriptionBuilder.Length > 0) descriptionBuilder.Append(", ");
                
                var desc = $"Cure: {modifier.Description}";
                descriptionBuilder.Append(desc);
            }
            
            foreach (var modifier in curses)
            {
                if (descriptionBuilder.Length > 0) descriptionBuilder.Append(", ");
                var desc = $"Curse: {modifier.Description}";
                descriptionBuilder.Append(desc);
            }
            
            description = descriptionBuilder.ToString();
        }
    }
}