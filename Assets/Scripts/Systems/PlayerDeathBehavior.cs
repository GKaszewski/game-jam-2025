using Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Systems
{
    public class PlayerDeathBehavior : MonoBehaviour, IDeathBehavior
    {
        public void Die(GameObject killer = null)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}