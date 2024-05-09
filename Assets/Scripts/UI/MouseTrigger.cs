using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Muvuca.UI
{
    public class MouseTrigger : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
    {
        [SerializeField] UnityEvent<Transform> mouseEntered;
        [SerializeField] UnityEvent<Transform> mouseExited;
        [SerializeField] UnityEvent<PointerEventData> mouseClicked;

        public void OnPointerClick(PointerEventData eventData)
        {
            mouseClicked.Invoke(eventData);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            mouseEntered.Invoke(transform);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            mouseExited.Invoke(transform);
        }
    }
}