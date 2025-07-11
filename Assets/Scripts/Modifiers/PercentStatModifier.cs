using Data;
using Interfaces;

namespace Modifiers
{
    public class PercentStatModifier : IStatModifier
    {
        private float lastAppliedAmount;

        public Stat Stat { get; set; }
        public float Percent { get; set; }
        public string Description => $"{Stat} +{Percent * 100}%";

        public void Apply(CharacterAttributes attributes)
        {
            var baseValue = GetBaseValue<float>(attributes);
            lastAppliedAmount = baseValue * Percent;
            
            var flatModifier = new FlatStatModifier
            {
                value = lastAppliedAmount,
                stat = Stat
            };
            
            flatModifier.Apply(attributes);
        }

        public void Remove(CharacterAttributes attributes)
        {
            var flatModifier = new FlatStatModifier
            {
                value = -lastAppliedAmount,
                stat = Stat
            };
            
            flatModifier.Apply(attributes);
        }

        private T GetBaseValue<T>(CharacterAttributes attributes)
        {
            return Stat switch
            {
                Stat.Health => (T)(object)attributes.Health,
                Stat.MaxHealth => (T)(object)attributes.MaxHealth,
                Stat.MoveSpeed => (T)(object)attributes.MoveSpeed,
                Stat.Luck => (T)(object)attributes.Luck,
                Stat.Armor => (T)(object)attributes.Armor,
                Stat.Damage => (T)(object)attributes.Damage,
                Stat.RangedDamage => (T)(object)attributes.RangedDamage,
                Stat.MeleeDamage => (T)(object)attributes.MeleeDamage,
                Stat.AttackRange => (T)(object)attributes.AttackRange,
                Stat.AttackSpeed => (T)(object)attributes.AttackSpeed,
                _ => throw new System.ArgumentOutOfRangeException()
            };
        }
    }
}