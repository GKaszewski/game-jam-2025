using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "Game/Wave")]
    public class EnemyWave : ScriptableObject
    {
        [TableList]
        [OdinSerialize] public List<EnemySpawnInfo> spawns = new();
    }

    [Serializable]
    public class EnemySpawnInfo
    {
        [HorizontalGroup("Row", width: 80)]
        [OdinSerialize, PreviewField(80)] public GameObject enemyPrefab;
        
        [HorizontalGroup("Row")]
        [OdinSerialize] public int count = 1;

        [HorizontalGroup("Row")] [OdinSerialize]
        public float spawnInterval = 0.5f;
    }
}