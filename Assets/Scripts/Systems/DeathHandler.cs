using Data;
using Interfaces;
using KBCore.Refs;
using UnityEngine;

namespace Systems
{
    public class DeathHandler : MonoBehaviour
    {
        [Self, SerializeField] private Character character;
        [Self, SerializeField] private InterfaceRef<IDeathBehavior> deathBehavior;
        [Self, SerializeField] private Health health;

        private void OnEnable()
        {
            character.attributes.OnHealthChanged += OnHealthChanged;
        }

        private void OnDisable()
        {
            character.attributes.OnHealthChanged -= OnHealthChanged;
        }

        private void OnHealthChanged(float newHealth)
        {
            if (newHealth <= 0f)
            {
                Die();
            }
        }

        private void Die()
        {
            var lastAttacker = health.LastAttacker;
            deathBehavior.Value.Die(lastAttacker);
        }
    }
}