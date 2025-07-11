using Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Systems
{
    public class PlayerDeathBehavior : MonoBehaviour, IDeathBehavior
    {
        public void Die()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}