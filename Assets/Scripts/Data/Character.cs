using System;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Data
{
    public class Character : SerializedMonoBehaviour
    {
        [OdinSerialize, NonSerialized] public CharacterAttributes attributes = new();

        private void Start()
        {
            attributes.Init();
        }
    }
}