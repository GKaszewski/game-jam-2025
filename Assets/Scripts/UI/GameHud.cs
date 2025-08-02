using System;
using Systems;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Attribute = Data.Attribute;

namespace UI
{
    public class GameHud : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI expText;
        [SerializeField] private TextMeshProUGUI coinsText;
        [SerializeField] private TextMeshProUGUI roundTimeLeftText;
        [SerializeField] private Slider healthSlider;

        private void Update()
        {
            var player = GameManager.Instance.Player;
            if (!player) return;
            
            expText.text = $"EXP: {player.attributes.Get(Attribute.Experience)}";
            coinsText.text = $"Coins: {GameManager.Instance.Coins}";
            roundTimeLeftText.text = $"{GameManager.Instance.RoundTimeLeft:F1}s";
            healthSlider.maxValue = player.attributes.Get(Attribute.MaxHealth);
            healthSlider.value = player.attributes.Get(Attribute.Health);
        }
    }
}