using JetBrains.Annotations;
using UnityEngine;

namespace Interfaces
{
    public interface IDeathBehavior
    {
        void Die([CanBeNull] GameObject killer = null);
    }
}