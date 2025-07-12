using System;
using System.Collections;
using System.Collections.Generic;
using Data;
using KBCore.Refs;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Systems
{
    public class EnemyWavesManager : MonoBehaviour
    {
        [OdinSerialize, ListDrawerSettings(NumberOfItemsPerPage = 20), SerializeField]
        private List<EnemyWave> waves = new();
        
        [OdinSerialize, SerializeField] private Transform[] spawnPoints;
        
        private Coroutine spawnCoroutine;
        private List<GameObject> aliveEnemies = new();
        [Self, SerializeField] private GameManager gameManager;

        private void OnEnable()
        {
            gameManager.OnRoundStart += StartWave;
            gameManager.OnRoundEnd += EndWave;
        }

        private void OnDisable()
        {
            gameManager.OnRoundStart -= StartWave;
            gameManager.OnRoundEnd -= EndWave;
        }

        private void StartWave(int round)
        {
            if (spawnCoroutine != null) StopCoroutine(spawnCoroutine);
            if (round - 1 < waves.Count) spawnCoroutine = StartCoroutine(SpawnWave(waves[round - 1]));
        }

        private void EndWave(int round)
        {
            foreach (var enemy in aliveEnemies)
            {
                if (!enemy) continue;
                Destroy(enemy);
            }
            
            aliveEnemies.Clear();
            if (spawnCoroutine != null) StopCoroutine(spawnCoroutine);
        }

        private IEnumerator SpawnWave(EnemyWave wave)
        {
            foreach (var spawn in wave.spawns)
            {
                for (var i = 0; i < spawn.count; i++)
                {
                    var spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                    var enemy = Instantiate(spawn.enemyPrefab, spawnPoint.position, Quaternion.identity);
                    aliveEnemies.Add(enemy);
                    yield return new WaitForSeconds(spawn.spawnInterval);
                }
            }
        }
    }
}