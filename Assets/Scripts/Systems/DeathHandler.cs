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
            deathBehavior.Value.Die();
        }
    }
}