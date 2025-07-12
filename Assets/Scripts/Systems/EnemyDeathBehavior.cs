using System;
using Interfaces;
using Sirenix.Serialization;
using UnityEngine;

namespace Systems
{
    public class EnemyDeathBehavior : MonoBehaviour, IDeathBehavior
    {
        public static event Action<GameObject, GameObject> OnAnyEnemyKilled;
        
        [OdinSerialize, SerializeField] private int expReward = 5;
        [OdinSerialize, SerializeField] private int coinReward = 1;
        
        public void Die(GameObject killer = null)
        {
            GameManager.Instance.Player.attributes.ModifyExperience(expReward);
            GameManager.Instance.AddCoins(coinReward);
            
            OnAnyEnemyKilled?.Invoke(killer ?? GameManager.Instance.Player.gameObject, gameObject);
            
            Destroy(gameObject);
            
            // later let's add particle effects, sound effects, etc.
        }
    }
}