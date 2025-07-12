using Sirenix.Serialization;
using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu(menuName = "Game/Item/WeaponItem")]
    public class WeaponItem : ScriptableObject
    {
        [OdinSerialize] public string weaponName;
        [OdinSerialize, TextArea] public string description;
        [OdinSerialize] public GameObject prefab;
        [OdinSerialize] public Sprite icon;
        [OdinSerialize] public int price;
    }
}