using System;
using Data;
using Interfaces;
using Attribute = Data.Attribute;

namespace Modifiers
{
    [Serializable]
    public class PercentStatModifier : IStatModifier
    {
        private float lastAppliedAmount;

        public Attribute stat;
        public float percent;
        public string Description => GetDescription();

        public void Apply(CharacterAttributes attributes)
        {
            var baseValue = attributes.Get(stat);
            lastAppliedAmount = baseValue * percent;
            attributes.Modify(stat, lastAppliedAmount);
        }

        public void Remove(CharacterAttributes attributes)
        {
            attributes.Modify(stat, -lastAppliedAmount);
        }
        
        private string GetDescription()
        {
            var sign = percent >= 0 ? "+" : "";
            return $"{stat} {sign}{percent * 100}%";
        }
    }
}