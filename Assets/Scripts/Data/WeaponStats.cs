using System;
using Interfaces;

namespace Data
{
    [Serializable]
    public class WeaponStats
    {
        public float damage;
        public float attackSpeed;
        public float range;
        public DamageType damageType;
    }
}