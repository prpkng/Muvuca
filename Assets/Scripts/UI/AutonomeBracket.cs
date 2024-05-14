namespace Muvuca.UI
{
    using DG.Tweening;
    using UnityEngine;

    public class AutonomeBracket : MonoBehaviour
    {
        private SelectionBracket bracket;
        private float startBracketsDistance;
        private void Awake()
        {
            bracket = GetComponent<SelectionBracket>();
            startBracketsDistance = bracket.bracketsDistance;
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
            if (target is RectTransform rect)
                dist = rect.rect.width / 2f + 10;
            bracket.bracketsDistance -= selectorSizeAnimForce;
            DOTween.To(() => bracket.bracketsDistance, s => bracket.bracketsDistance = s, dist, selectorSizeAnimDuration).SetTarget(bracket).SetEase(Ease.OutCubic);
        }

        public void UnselectAll()
        {
            bracket.gameObject.SetActive(false);
            bracket.bracketsDistance = startBracketsDistance;
        }
    }
}