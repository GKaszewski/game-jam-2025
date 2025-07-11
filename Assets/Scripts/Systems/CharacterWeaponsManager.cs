using System.Collections.Generic;
using Sirenix.Serialization;
using UnityEngine;
using Weapons;

namespace Systems
{
    public class CharacterWeaponsManager : MonoBehaviour
    {
        [OdinSerialize] private List<Weapon> equippedWeapons = new();
    }
}