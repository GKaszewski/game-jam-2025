using Data;
using JetBrains.Annotations;

namespace Interfaces
{
    public interface IDamageInflectorSetup
    {
        void Setup(Character attacker, [CanBeNull] WeaponStats weaponStats = null);
    }
}