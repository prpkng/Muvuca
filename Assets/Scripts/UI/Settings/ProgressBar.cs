using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Muvuca.UI.Settings
{
    public class ProgressBar : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public float step = 0.1f;
        public Canvas canvas;
        public UnityEvent<float> valueChanged;

        
        private bool isHolding;
        [SerializeField] private RectTransform progressBarRect;

        private RectTransform rectTransform;
        [SerializeField] private string playerPrefsKey = "GeneralVolume";

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            if (PlayerPrefs.HasKey(playerPrefsKey))
                progressBarRect.localScale = new Vector3(PlayerPrefs.GetFloat(playerPrefsKey), 1f);
        }

        private void Update()
        {
            if (!isHolding) return;
            RectTransformUtility.ScreenPointToWorldPointInRectangle(
                (RectTransform)canvas.transform,
                Mouse.current.position.ReadValue(),
                null, out var mousePos
                );

            var mouseX = mousePos.x - (rectTransform.position.x + rectTransform.rect.xMin);
            mouseX = Mathf.Clamp(mouseX, 0, rectTransform.rect.width) / rectTransform.rect.width;
            var value = Mathf.Round(mouseX * (1f / step)) * step;
            valueChanged.Invoke(value);
            progressBarRect.localScale = new Vector3(value, 1f);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            isHolding = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            isHolding = false;
        }
    }
}