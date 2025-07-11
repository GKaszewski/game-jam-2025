using System;
using Data;
using KBCore.Refs;
using UnityEngine;

namespace Systems
{
    public class Health : MonoBehaviour
    {
        [Self, SerializeField] private Character character;
        [SerializeField] private float initialHealth = 100f;

        private void Start()
        {
            character.attributes.SetHealth(initialHealth);
        }

        public void TakeDamage(float damage)
        {
            var effectiveDamage = Math.Max(damage - character.attributes.Armor, 1);
            character.attributes.ModifyHealth(-effectiveDamage);
        }
    }
}