using System;
using KBCore.Refs;
using Unity.Cinemachine;
using UnityEngine;

namespace Systems
{
    public class CameraShake : MonoBehaviour
    {
        private float timer;
        private bool isShaking;
        
        [SerializeField] private float duration;
        [SerializeField, Self] private CinemachineBasicMultiChannelPerlin noise;
        [SerializeField] private Health health;

        private void OnEnable()
        {
            health.OnTakeDamage += Shake;
        }

        private void OnDisable()
        {
            health.OnTakeDamage -= Shake;
        }

        private void Update()
        {
            if (!isShaking || !noise) return;
            if (timer <= 0f)
            {
                isShaking = false;
                noise.AmplitudeGain = 0f;
            } else
            {
                timer -= Time.deltaTime;
            }
        }
        
        private void Shake() 
        {
            if (!noise) return;
            isShaking = true;
            timer = duration;
            noise.AmplitudeGain = 1f;
        }
    }
}