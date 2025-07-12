using Interfaces;
using Sirenix.Serialization;
using UnityEngine;

namespace Systems
{
    public class EnemyDeathBehavior : MonoBehaviour, IDeathBehavior
    {
        [OdinSerialize, SerializeField] private int expReward = 5;
        [OdinSerialize, SerializeField] private int coinReward = 1;
        
        public void Die()
        {
            GameManager.Instance.Player.attributes.ModifyExperience(expReward);
            GameManager.Instance.AddCoins(coinReward);
            Destroy(gameObject);
            
            // later let's add particle effects, sound effects, etc.
        }
    }
}