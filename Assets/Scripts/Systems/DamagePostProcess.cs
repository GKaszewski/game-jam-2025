using System.Collections;
using KBCore.Refs;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Systems
{
    public class DamagePostProcess : MonoBehaviour
    {
        private float ogVignetteIntensity;
        private Coroutine runningEffect;
        
        [SerializeField, Scene] private Volume globalVolume;
        [SerializeField, Self] private Health health;
        [SerializeField] private float postProcessDuration = 0.5f;
        
        [SerializeField] private float vignetteHitIntensity = 0.5f;
        
        private void OnEnable()
        {
            health.OnTakeDamage += OnHit;
        }

        private void OnDisable()
        {
            health.OnTakeDamage -= OnHit;
        }

        private void OnHit()
        {
            if (runningEffect != null) StopCoroutine(runningEffect);
            
            runningEffect = StartCoroutine(ApplyPostProcessEffect());
        }

        private IEnumerator ApplyPostProcessEffect()
        {
            if (!globalVolume) yield break;

            if (!globalVolume.profile.TryGet<Vignette>(out var vignette))
                yield break;

            ogVignetteIntensity = vignette.intensity.value;
            globalVolume.profile.TryGet<ChromaticAberration>(out var chromaticAberration);
            
            vignette.intensity.value = vignetteHitIntensity;
            if (chromaticAberration) chromaticAberration.active = true;
            
            yield return new WaitForSeconds(postProcessDuration);
            
            float fade = 0f, fadeDuration = 0.25f;
            while (fade < fadeDuration)
            {
                fade += Time.deltaTime;
                vignette.intensity.value = Mathf.Lerp(vignetteHitIntensity, ogVignetteIntensity, fade / fadeDuration);
                yield return null;
            }
            
            vignette.intensity.value = ogVignetteIntensity;
            if (chromaticAberration) chromaticAberration.active = false;

            runningEffect = null;
        }
    }
}