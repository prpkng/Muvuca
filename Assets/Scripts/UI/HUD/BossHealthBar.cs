namespace Muvuca.UI.HUD
{
    using System.Collections.Generic;
    using Cysharp.Threading.Tasks;
    using DG.Tweening;
    using Muvuca.Game.Elements.Enemies;
    using Muvuca.Tools;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    public class BossHealthBar : MonoBehaviour
    {
        [ReadOnly] public int health;
        public RectTransform healthIndicatorsParent;

        [Header("Generation")]
        [SerializeField] private float spacing;
        [SerializeField] private RectTransform healthStripGameObject;
        [SerializeField] private UnityEvent onHealthRemoved;

        [ReadOnly][SerializeField] private List<Image> healthIndicators;

        [Header("Properties")]
        [SerializeField] private Sprite activeSpr;
        [SerializeField] private Sprite inactiveSpr;
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
            BossController.BossGotHit += SetHealth;
        }

        private void OnDisable()
        {
            BossController.BossGotHit -= SetHealth;
        }

        private async void SetHealth()
        {
            var amount = BossController.CurrentInstance.health.currentHp;
            while (amount >= health)
            {
                healthIndicators[health].transform.DOShakePosition(shakeDuration, shakeStrength, shakeVibrato, shakeRandomness);

                health++;
                var _i = 0;
                foreach (var healthIndicator in healthIndicators)
                {
                    _i++;
                    healthIndicator.sprite = health >= _i ? activeSpr : inactiveSpr;
                }
                await UniTask.WaitForSeconds(.1f);
            }

            while (amount < health)
            {
                healthIndicators[health - 1].transform.DOShakePosition(shakeDuration, shakeStrength, shakeVibrato, shakeRandomness);
                health--;
            }

            transform.DOKill(true);
            transform.DOScale(Vector3.one * scaleMagnitude, scaleDuration).SetEase(scaleEase);
            onHealthRemoved.Invoke();

            var i = 0;
            foreach (var healthIndicator in healthIndicators)
            {
                i++;
                healthIndicator.sprite = health >= i ? activeSpr : inactiveSpr;
            }
        }
    }
}