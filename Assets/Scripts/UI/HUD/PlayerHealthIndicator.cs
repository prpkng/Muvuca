using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Muvuca.Core;
using Muvuca.Effects;
using Muvuca.Tools;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Muvuca.UI.HUD
{
    public class PlayerHealthIndicator : MonoBehaviour
    {
        [ReadOnly] private int health;
        public Transform healthIndicatorsParent;
        
        [Header("Generation")]
        public int maxHealth;
        [SerializeField] private float spacing;
        [SerializeField] private GameObject healthStripGameObject;

        [ReadOnly] [SerializeField] private List<Image> healthIndicators;
        public void SetMaxHealth(int value)
        {
            for(var i = healthIndicatorsParent.childCount - 1; i >= 0; i--)
                Destroy(healthIndicatorsParent.GetChild(i).gameObject);
            var currentZ = -spacing/2f;
            var barSize = (360f - spacing * value) / value;  
            for (int i = 0; i < value; i++)
            {
                var img = Instantiate(healthStripGameObject, healthIndicatorsParent).GetComponent<Image>();
                img.fillAmount = barSize / 360f;
                img.transform.eulerAngles = Vector3.forward * currentZ;
                currentZ -= barSize + spacing;
                healthIndicators.Add(img);
            }

            healthIndicators.Reverse();
            SetHealth();
        }

        private void Start()
        {
            SetMaxHealth(PlayerController.Instance.health.currentHp);
        }

        [Header("Properties")]
        [SerializeField] private Color activeColor;
        [SerializeField] private Color inactiveColor;
        [SerializeField] private float[] huePoints;
        [SerializeField] private float shakeDuration = .1f;
        [SerializeField] private float shakeStrength = 10f;
        [SerializeField] private int shakeVibrato = 10;
        [SerializeField] private float shakeRandomness = 90f;
        [Space] 
        [SerializeField] private float scaleMagnitude;
        [SerializeField] private float scaleDuration;
        [SerializeField] private AnimationCurve scaleEase;
        private void OnEnable()
        {
            PlayerController.PlayerHealthChanged += SetHealth;
        }

        private void OnDisable()
        {
            PlayerController.PlayerHealthChanged -= SetHealth;
        }

        private void SetHealth()
        {
            var amount = PlayerController.Instance.health.currentHp;
            if (amount > health){
                health = amount;
                return;
            }
            
            while (amount < health)
            {
                healthIndicators[health - 1].transform.DOShakePosition(shakeDuration, shakeStrength, shakeVibrato, shakeRandomness);
                transform.DOKill(true);
                transform.DOScale(Vector3.one * scaleMagnitude, scaleDuration).SetEase(scaleEase);
                health--;
            }

            health = Mathf.Clamp(health, 0, maxHealth); 
            
                
            
            var i = 0;
            foreach (var healthIndicator in healthIndicators)
            {
                i++;
                var clr = health >= i ? activeColor : inactiveColor;
                var a = clr.a;
                float h, s, v;
                Color.RGBToHSV(clr, out h, out s, out v);
                h = huePoints[health-1] / 360f;
                clr = Color.HSVToRGB(h, s, v);
                clr.a = a;
                healthIndicator.color = clr;
                
                if (!healthIndicator.TryGetComponent(out Flashing f)) continue;
                
                if (health == 1)
                    f.Play();
                else
                    f.Stop();
            }
        }
    }
}