using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Data
{
    public class Character : MonoBehaviour
    {
        [OdinSerialize, InlineProperty] public CharacterAttributes attributes = new();
    }
}