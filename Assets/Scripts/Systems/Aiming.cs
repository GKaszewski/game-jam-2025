using System;
using KBCore.Refs;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Systems
{
    public class Aiming : MonoBehaviour
    {
        [SerializeField, Scene] private Camera mainCamera;

        private void Reset()
        {
            if (!mainCamera)
            {
                mainCamera = Camera.main;
            }
        }

        private void Update()
        {
            if (!mainCamera) return;
            var mousePosition = Mouse.current.position.ReadValue();
            
            var worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);
            worldPosition.z = transform.position.z;
            
            var direction = (worldPosition - transform.position).normalized;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            
            transform.rotation = Quaternion.Euler(0,0, angle);
        }
    }
}