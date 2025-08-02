using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace Data
{
    [Serializable]
    public class AttributeData<T>
    {
        [OdinSerialize]
        public T Value { get; private set; }
        public event Action<T> OnChanged;

        public void Set(T value)
        {
            if (!EqualityComparer<T>.Default.Equals(Value, value))
            {
                Value = value;
                OnChanged?.Invoke(Value);
            }
        }

        public void Modify(Func<T, T> modifier)
        {
            Set(modifier(Value));
        }
    }
}