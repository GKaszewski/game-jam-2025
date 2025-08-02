using System;
using Sirenix.OdinInspector;

namespace Data
{
    [Serializable]
    public class AttributeEntry
    {
        [HorizontalGroup("Split", 0.4f)]
        [LabelWidth(100)]
        public Attribute key;
        
        [HorizontalGroup("Split", 0.6f)]
        [LabelWidth(50)]
        [InlineProperty]
        public AttributeData<float> value;
    }
}