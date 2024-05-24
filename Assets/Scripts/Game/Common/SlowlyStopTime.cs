using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace Muvuca.Game.Common
{
    public class SlowlyStopTime : MonoBehaviour
    {

        public float duration;
        public Ease easing;
        public UnityEvent onFinished;
        
        public void Run()
        {
            DOTween.To(f => Time.timeScale = f, 1f, 0f, duration).SetUpdate(true).SetEase(easing).onComplete += () => onFinished.Invoke();
        }
    }
}