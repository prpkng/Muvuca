using TMPro;
using UnityEngine;

namespace Muvuca.UI.Menu
{
    public class CurrentSelectedTint : MonoBehaviour
    {
        [SerializeField] private int index;
        [SerializeField] private MenuSectionActivator sectionActivator;

        [SerializeField] private Color selectedColor;
        [SerializeField] private Color unselectedColor;

        private TMP_Text text;

        private void Awake() => text = GetComponent<TMP_Text>();


        private void Update()
        {
            text.color = sectionActivator.selection == index ? selectedColor : unselectedColor;
        }
    }
}