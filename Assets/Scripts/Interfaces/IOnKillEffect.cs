using UnityEngine;

namespace Interfaces
{
    public interface IOnKillEffect
    {
        void OnKill(GameObject killer, GameObject victim);
    }
}