using System;
using System.Collections;
using KBCore.Refs;
using UnityEngine;

namespace Systems
{
    public class DamageFlash : MonoBehaviour
    {
        private static readonly int MarkerFlashColor = Shader.PropertyToID("_FlashColor");
        private static readonly int MarkerFlashAmount = Shader.PropertyToID("_FlashAmount");
        private Material material;
        private Coroutine damageFlashCoroutine;
        
        [SerializeField] private float flashDuration;
        [ColorUsage(true, true)][SerializeField] private Color flashColor = Color.white;
        [SerializeField, Self] private Health health;
        [SerializeField, Child] private SpriteRenderer spriteRenderer;
        
        private void OnEnable()
        {
            health.OnTakeDamage += OnHit;
            
            Initialize();
        }

        private void OnDisable()
        {
            health.OnTakeDamage -= OnHit;
        }

        private void Initialize()
        {
            if (!spriteRenderer) return;
            
            material = spriteRenderer.material;
        }

        private void OnHit()
        {
            damageFlashCoroutine = StartCoroutine(Flash());
        }

        private IEnumerator Flash()
        {
            SetFlashColor(flashColor);
            var elapsedTime = 0f;

            while (elapsedTime < flashDuration)
            {
                elapsedTime += Time.deltaTime;
                var currentFlashAmount = Mathf.Lerp(1f, 0f, elapsedTime / flashDuration);
                material.SetFloat(MarkerFlashAmount, currentFlashAmount);
                yield return null;
            }
        }
        
        private void SetFlashColor(Color color)
        {
            if (!material) return;
            material.SetColor(MarkerFlashColor, color);
        }
    }
}