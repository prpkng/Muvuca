namespace Muvuca.UI
{
    using DG.Tweening;
    using UnityEngine;

    public class AutonomeBracket : MonoBehaviour
    {
        private SelectionBracket bracket;
        private void Awake()
        {
            bracket = GetComponent<SelectionBracket>();
            gameObject.SetActive(false);
        }

        [SerializeField] private float selectorMoveDuration;
        [SerializeField] private float selectorSizeAnimForce = 20;
        [SerializeField] private float selectorSizeAnimDuration = .5f;

        public void SelectTransform(Transform target)
        {
            bracket.gameObject.SetActive(true);
            bracket.transform.DOKill();
            bracket.transform.DOMove(target.position, selectorMoveDuration).SetEase(Ease.OutCubic);
            bracket.DOKill(true);
            var dist = bracket.bracketsDistance;
            bracket.bracketsDistance -= selectorSizeAnimForce;
            DOTween.To(() => bracket.bracketsDistance, s => bracket.bracketsDistance = s, dist, selectorSizeAnimDuration).SetTarget(bracket).SetEase(Ease.OutCubic);
        }

        public void UnselectAll()
        {
            bracket.gameObject.SetActive(false);
        }
    }
}