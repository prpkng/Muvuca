namespace Muvuca.UI.Settings
{
    using DG.Tweening;
    using UnityEngine;

    public class SettingsMenuToggle : MonoBehaviour
    {
        public float showDuration;
        public Ease showEase;
        public float hideDuration;
        public Ease hideEase;

        [SerializeField] private CanvasGroup canvasGroup;

        public void Show()
        {
            transform.localScale = Vector3.one * 1.1f;
            transform.DOScale(1f, showDuration).SetEase(showEase);
            canvasGroup.DOFade(1f, showDuration).SetEase(showEase);
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }

        public void Hide()
        {
            transform.DOScale(1.1f, hideDuration).SetEase(hideEase);
            canvasGroup.DOFade(0f, hideDuration).SetEase(hideEase);
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }
}