using System.Collections.Generic;
using DG.Tweening;
using Muvuca.Tools;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Muvuca.UI.Menu
{
    public class MenuSectionActivator : MonoBehaviour, IPointerExitHandler
    {
        public GameObject[] sections;
        [ReadOnly] public Transform[] sectionButtons;
        [SerializeField] private Transform currentSelectedHighlight;
        [SerializeField] private SelectionBracket selector;
        public int selection;

        [Space]
        [SerializeField] private float selectorMoveDuration;
        [SerializeField] private float selectorSizeAnimForce = 20;
        [SerializeField] private float selectorSizeAnimDuration = .5f;

        private void Awake()
        {
            var transforms = new List<Transform>();
            foreach (Transform child in transform) transforms.Add(child);
            sectionButtons = transforms.ToArray();
            SelectionChanged();
        }

        private void SelectionChanged()
        {
            for (int i = 0; i < sections.Length; i++)
                sections[i].SetActive(i == selection);
        }

        public void MouseEntered(int index)
        {
            selector.gameObject.SetActive(true);
            selector.transform.DOKill();
            selector.transform.DOMove(sectionButtons[index].position, selectorMoveDuration).SetEase(Ease.OutCubic);
            selector.DOKill(true);
            var dist = selector.bracketsDistance;
            selector.bracketsDistance -= selectorSizeAnimForce;
            DOTween.To(() => selector.bracketsDistance, s => selector.bracketsDistance = s, dist, selectorSizeAnimDuration).SetTarget(selector).SetEase(Ease.OutCubic);
        }

        public void MouseClicked(int index)
        {
            currentSelectedHighlight.position = sectionButtons[index].position;
            selection = index;
            SelectionChanged();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            selector.gameObject.SetActive(false);
        }
    }
}
