using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Muvuca.UI
{
    public class MenuTextButton : MonoBehaviour
    {
        [SerializeField] private float selectedFontSize = 44f;
        [SerializeField] private float unselectedFontSize = 24f;

        [SerializeField] private float scalingSpeed = 4f;

        public bool selected;

        [HideInInspector] public TMP_Text tmp;
        private void Awake() {
            tmp = GetComponent<TMP_Text>();
        }

        private void Update() {
            tmp.fontSize = Mathf.Lerp(tmp.fontSize, selected ? selectedFontSize : unselectedFontSize, Time.deltaTime * scalingSpeed);
        }
    }
}
