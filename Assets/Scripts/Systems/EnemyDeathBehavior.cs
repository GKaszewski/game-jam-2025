using Interfaces;
using UnityEngine;

namespace Systems
{
    public class EnemyDeathBehavior : MonoBehaviour, IDeathBehavior
    {
        public void Die()
        {
            Destroy(gameObject);
            // later let's add particle effects, sound effects, etc.
            // and give player experience points
        }
    }
}