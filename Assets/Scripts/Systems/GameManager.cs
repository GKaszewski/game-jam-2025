using System;
using System.Collections;
using Data;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Systems
{
    public class GameManager : MonoBehaviour
    {
        private float timer;
        
        public static GameManager Instance { get; private set; }

        [OdinSerialize, SerializeField] private int currentRound = 1;
        [OdinSerialize, SerializeField] private int coins = 0;
        [OdinSerialize, SerializeField] private float roundTime = 60f;
        [OdinSerialize, SerializeField] private int maxRounds = 20;
        [OdinSerialize, SerializeField] private Scene winScene;
        [OdinSerialize, SerializeField] private Transform arenaCenter;
        
        [OdinSerialize, SerializeField] private Character player;
        
        public Character Player => player;
        public int Coins => coins;
        public int CurrentRound => currentRound;
        public float RoundTime => roundTime;
        public int MaxRounds => maxRounds;
        public bool StoreIsClosed { get; set; } = true;
        public float RoundTimeLeft => Mathf.Max(0, timer);

        public event Action<int> OnRoundStart;
        public event Action<int> OnRoundEnd;
        public event Action OnStoreOpen;
        

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        private void Start()
        {
            StartCoroutine(RoundLoop());
        }

        private IEnumerator RoundLoop()
        {
            OnStoreOpen?.Invoke();
            yield return new WaitUntil(() => StoreIsClosed);
            
            for (currentRound = 1; currentRound <= maxRounds; currentRound++)
            {
                player.transform.position = new Vector3(arenaCenter.position.x, arenaCenter.position.y, player.transform.position.z);
                OnRoundStart?.Invoke(currentRound);
                timer = roundTime;

                while (timer > 0)
                {
                    timer -= Time.deltaTime;
                    yield return null;
                }

                OnRoundEnd?.Invoke(currentRound);
                StoreIsClosed = false;
                OnStoreOpen?.Invoke();
                yield return new WaitUntil(() => StoreIsClosed);
            }
            
            SceneManager.LoadScene(winScene.name);
        }
        
        public void AddCoins(int amount)
        {
            coins += amount;
        }
        
        public void SpendCoins(int amount)
        {
            if (coins >= amount)
            {
                coins -= amount;
            }
        }
    }
}