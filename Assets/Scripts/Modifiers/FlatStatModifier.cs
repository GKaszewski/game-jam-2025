using System;
using Data;
using Interfaces;
using Attribute = Data.Attribute;

namespace Modifiers
{
    [Serializable]
    public class FlatStatModifier : IStatModifier
    {
        public Attribute stat;
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
                case Attribute.Health:
                    attributes.Modify(Attribute.Health, value);
                    break;
                case Attribute.MaxHealth:
                    attributes.Modify(Attribute.MaxHealth, value);
                    break;
                case Attribute.MoveSpeed:
                    attributes.Modify(Attribute.MoveSpeed, value);
                    break;
                case Attribute.Luck:
                    attributes.Modify(Attribute.Luck, value);
                    break;
                case Attribute.Armor:
                    attributes.Modify(Attribute.Armor, value);
                    break;
                case Attribute.Damage:
                    attributes.Modify(Attribute.Damage, value);
                    break;
                case Attribute.RangedDamage:
                    attributes.Modify(Attribute.RangedDamage, value);
                    break;
                case Attribute.MeleeDamage:
                    attributes.Modify(Attribute.MeleeDamage, value);
                    break;
                case Attribute.AttackRange:
                    attributes.Modify(Attribute.AttackRange, value);
                    break;
                case Attribute.AttackSpeed:
                    attributes.Modify(Attribute.AttackSpeed, value);
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