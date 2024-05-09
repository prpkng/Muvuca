namespace Muvuca.UI
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using DG.Tweening;
    using UnityEngine;

    public class SelectionBracket : MonoBehaviour
    {
        public float bracketsDistance;

        private Transform[] brackets;


        private void Awake()
        {
            var transforms = new List<Transform>();
            foreach (Transform child in transform) transforms.Add(child);
            brackets = transforms.ToArray();
        }

        private void Update()
        {
            brackets[0].localPosition = Vector2.left * bracketsDistance;
            brackets[1].localPosition = Vector2.right * bracketsDistance;
        }
    }
}