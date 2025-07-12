using System;
using Data;
using Interfaces;

namespace Modifiers
{
    [Serializable]
    public class FlatStatModifier : IStatModifier
    {
        public Stat stat;
        public float value;
        public string Description => BuildDescription();
        
        public void Apply(CharacterAttributes attributes)
        {
            ModifyAttributes(attributes, value);
        }

        public void Remove(CharacterAttributes attributes)
        {
            Apply(attributes, -value);
        }

        private void Apply(CharacterAttributes attributes, float value)
        {
            ModifyAttributes(attributes, value);
        }
        
        private void ModifyAttributes(CharacterAttributes attributes, float value)
        {
            switch (stat)
            {
                case Stat.Health:
                    attributes.ModifyHealth(value);
                    break;
                case Stat.MaxHealth:
                    attributes.ModifyMaxHealth(value);
                    break;
                case Stat.MoveSpeed:
                    attributes.ModifyMoveSpeed(value);
                    break;
                case Stat.Luck:
                    attributes.ModifyLuck(value);
                    break;
                case Stat.Armor:
                    attributes.ModifyArmor(value);
                    break;
                case Stat.Damage:
                    attributes.ModifyDamage(value);
                    break;
                case Stat.RangedDamage:
                    attributes.ModifyRangedDamage(value);
                    break;
                case Stat.MeleeDamage:
                    attributes.ModifyMeleeDamage(value);
                    break;
                case Stat.AttackRange:
                    attributes.ModifyAttackRange(value);
                    break;
                case Stat.AttackSpeed:
                    attributes.ModifyAttackSpeed(value);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private string BuildDescription()
        {
            var sign = value >= 0 ? "+" : "";
            return $"{stat} {sign}{value}";
        }
    }
}