using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Muvuca.Tools;
using UnityEngine;
using UnityEngine.Events;

namespace Muvuca.UI.Menu
{
    public class ListMenuController : MonoBehaviour
    {
        public UnityEvent[] actions;
        [ReadOnly] public MenuTextButton[] textButtons;
        public int selection;

        private float startY;

        private void Awake()
        {
            startY = transform.localPosition.y;
            var transforms = new List<Transform>();
            foreach (Transform child in transform) transforms.Add(child);
            textButtons = transforms.Select(t => t.GetComponent<MenuTextButton>()).ToArray();
            textButtons[selection].selected = true;
        }

        private void SelectionChanged()
        {
            for (var i = 0; i < textButtons.Length; i++)
                textButtons[i].selected = selection == i;

            transform.DOLocalMoveY(startY + (selection - 1) * 15, 0.4f).SetEase(Ease.OutExpo);
        }

        public void MouseEntered(int index)
        {
            selection = index;
            SelectionChanged();
        }

        public void MouseClicked()
        {
            actions[selection].Invoke();
        }

        private void OnDestroy()
        {
            transform.DOKill();
        }


    }
}
