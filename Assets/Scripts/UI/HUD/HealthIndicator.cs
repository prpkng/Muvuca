using System;
using System.Linq;
using DG.Tweening;
using Muvuca.Player;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Muvuca.UI.HUD
{
    public class HealthIndicator : MonoBehaviour
    {
        [SerializeField] private Transform healthIndicatorsParent;

        private Image[] healthIndicators = {};

        [SerializeField] private Color activeColor;
        [SerializeField] private Color inactiveColor;
        [SerializeField] [Range(1, 3)] private int health;
        [SerializeField] private float shakeDuration = .1f;
        [SerializeField] private float shakeStrength = 10f;
        [SerializeField] private int shakeVibrato = 10;
        [SerializeField] private float shakeRandomness = 90f;
        private void Awake()
        {
            var indicators = healthIndicators.AsEnumerable();
            indicators = healthIndicatorsParent
                .Cast<Transform>()
                .Aggregate(indicators, (current, child) => 
                    current.Append(child.GetComponent<Image>()));
            healthIndicators = indicators.ToArray();
        }

        private void OnEnable()
        {
            PlayerController.PlayerGotHit += SetHealth;
        }

        private void OnDisable()
        {
            PlayerController.PlayerGotHit -= SetHealth;
        }

        private void SetHealth()
        {
            var amount = PlayerController.Instance.health.currentHealthPoints;
            if (amount > health){
                health = amount;
                return;
            }
            
            while (amount < health)
            {
                healthIndicators[health - 1].transform.DOShakePosition(shakeDuration, shakeStrength, shakeVibrato, shakeRandomness);
                health--;
            }
        }
        
        private void Update()
        {
            int i = 0;
            foreach (var healthIndicator in healthIndicators)
            {
                i++;
                healthIndicator.color = health >= i ? activeColor : inactiveColor;
            }
        }
    }
}