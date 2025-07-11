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
        
        public List<IStatModifier> cures = new();
        public List<IStatModifier> curses = new();
    }
}