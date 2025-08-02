using System;
using Data;
using KBCore.Refs;
using UnityEngine;
using Attribute = Data.Attribute;

namespace Systems
{
    public class Health : MonoBehaviour
    {
        private GameObject lastAttacker;
        
        [Self, SerializeField] private Character character;
        [SerializeField] private float initialHealth = 100f;
        [SerializeField] private AudioClip damageSound;
        
        public GameObject LastAttacker => lastAttacker;

        public event Action OnTakeDamage;

        private void Start()
        {
            character.attributes.Set(Attribute.Health, initialHealth);
        }

        public void TakeDamage(float damage, GameObject attacker = null)
        {
            lastAttacker = attacker;
            
            var effectiveDamage = Math.Max(damage - character.attributes.Get(Attribute.Armor), 1);
            character.attributes.Modify(Attribute.Health, -effectiveDamage);
            
            if (damageSound)
            {
                AudioSource.PlayClipAtPoint(damageSound, transform.position);
            }
            
            OnTakeDamage?.Invoke();
        }
    }
}