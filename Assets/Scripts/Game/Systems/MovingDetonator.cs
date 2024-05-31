namespace Muvuca.Systems
{
    using System;
    using DG.Tweening;
    using Muvuca.Game.Common;
    using UnityEngine;
    using UnityEngine.Events;

    public class MovingDetonator : MonoBehaviour
    {
        public UnityEvent onReachEnd;
        public MoveIntoDir moving;

        [SerializeField] private float stopDuration;

        public static event Action OnReachEnd;
        public static void ReachEnd() => OnReachEnd?.Invoke();

        private void OnEnable()
        {
            OnReachEnd += ReachedEnd;
        }

        private void OnDisable()
        {
            OnReachEnd += ReachedEnd;
        }

        public void ReachedEnd()
        {
            DOTween.To(f => moving.speed = f, moving.speed, 0f, stopDuration).SetEase(Ease.OutExpo).SetTarget(moving);
            onReachEnd.Invoke();
        }
    }
}