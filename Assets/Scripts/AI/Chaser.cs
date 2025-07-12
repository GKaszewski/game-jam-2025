using System;
using Data;
using KBCore.Refs;
using Pathfinding;
using Systems;
using UnityEngine;

namespace AI
{
    public class Chaser : MonoBehaviour
    {
        [SerializeField, Self] private Character character;
        [SerializeField, Self] private AIPath aiPath;
        [SerializeField] private Transform target;

        private void OnEnable()
        {
            SetTarget();
        }

        private void Start()
        {
            SetTarget();
        }

        private void Update()
        {
            if (!target || !aiPath) return;

            aiPath.maxSpeed = character.attributes.MoveSpeed;
            aiPath.destination = target.position;
        }

        private void SetTarget()
        {
            target = GameManager.Instance.Player.transform;
        }
    }
}