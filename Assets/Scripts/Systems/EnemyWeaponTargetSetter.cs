using System.Collections.Generic;
using UnityEngine;
using Weapons;

namespace Systems
{
    public class EnemyWeaponTargetSetter : MonoBehaviour
    {
        [SerializeField] private List<AutoWeapon> weapons = new();
        [SerializeField] private Transform target;

        private void Reset()
        {
            if (weapons.Count == 0)
            {
                weapons = new List<AutoWeapon>(GetComponentsInChildren<AutoWeapon>());
            }
        }

        private void Update()
        {
            if (!target || weapons.Count == 0) return;

            foreach (var weapon in weapons)
            {
                weapon.Target = target.position;
            }
        }
    }
}