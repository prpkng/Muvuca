using DG.Tweening;
using Muvuca.Core;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Muvuca.UI
{
    public class PauseButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public UnityEvent onClick;
        public void OnPointerEnter(PointerEventData eventData)
        {
            InputManager.IgnoringMouse = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            InputManager.IgnoringMouse = false;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            onClick.Invoke();
        }
    }
}