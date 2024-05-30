using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Muvuca.Effects
{
    public class DoFade : MonoBehaviour
    {
        [SerializeField] private Graphic graphic;
        public bool thenDestroy;

        public float duration;
        public Ease easing;

        public void FadeOut()
        {
            var tween = graphic.DOFade(0, duration).SetEase(easing);
            if (thenDestroy)
                tween.onComplete += () => Destroy(gameObject);
        }

        public void FadeIn()
        {
            var clr = graphic.color;
            clr.a = 0f;
            graphic.color = clr;
            graphic.DOFade(1, duration).SetEase(easing);
        }
    }
}