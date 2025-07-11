using UnityEngine;

namespace Interfaces
{
    public interface IDamageInflector
    {
        float Damage { get; }
        GameObject Owner { get; }
        DamageType Type { get; }
    }
    
    public enum DamageType
    {
        Melee,
        Ranged,
        Magic,
        True
    }
}