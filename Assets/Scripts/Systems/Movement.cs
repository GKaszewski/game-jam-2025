using System;
using Data;
using KBCore.Refs;
using UnityEngine;
using UnityEngine.InputSystem;
using Attribute = Data.Attribute;

namespace Systems
{
    public class Movement : MonoBehaviour
    {
        private InputSystem_Actions controls;
        private Vector2 movementInput;
        
        [Self, SerializeField] private Character character;
        [Self, SerializeField] private Rigidbody2D rb;

        private void OnEnable()
        {
            controls ??= new InputSystem_Actions();
            controls.Enable();
            controls.Player.Move.performed += OnMovePerformed;
            controls.Player.Move.canceled += ctx => movementInput = Vector2.zero;
            
        }

        private void OnMovePerformed(InputAction.CallbackContext obj)
        {
            movementInput = obj.ReadValue<Vector2>();
        }

        private void OnDisable()
        {
            controls.Disable();
            controls.Player.Move.performed -= OnMovePerformed;
        }

        private void FixedUpdate()
        {
            ApplyMovement();
        }

        private void ApplyMovement()
        {
            if (!rb) return;
            
            var velocity = new Vector2(movementInput.x, movementInput.y).normalized * character.attributes.Get(Attribute.MoveSpeed);
            rb.linearVelocity = velocity;
        }
    }
}