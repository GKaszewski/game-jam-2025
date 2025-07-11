using System;
using Sirenix.Serialization;
using UnityEngine;

namespace Inventory
{
    [Serializable]
    public class InventorySlot
    {
        [OdinSerialize] public ScriptableObject item;
        [OdinSerialize] public int quantity;
    }
}