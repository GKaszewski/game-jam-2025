using Data;
using JetBrains.Annotations;

namespace Interfaces
{
    public interface IDamageInflectorSetup
    {
        void Setup(Character attacker, float damage, [CanBeNull] WeaponStats weaponStats = null);
    }
}