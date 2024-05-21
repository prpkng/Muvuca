using System;
using System.Linq;
using DG.Tweening;
using Muvuca.Core;
using Muvuca.Effects;
using UnityEngine;

namespace Muvuca.UI.HUD
{
    public class HoverSelectionBracket : MonoBehaviour 
    {
        
        public static Vector2 HoverSelectionDestination;

        private static float _bracketsDistance;
        private static float _currentBracketsDistance;
        private static Action<float> _changedBracketsDistance;

        public static float BracketsDistance
        {
            set
            {
                if (!InputManager.IgnoringMouse) 
                    CustomCursor.IsHovering = !Mathf.Approximately(value, -1);
                if (!Mathf.Approximately(_bracketsDistance, value))
                    _changedBracketsDistance?.Invoke(value);
                _bracketsDistance = value;
            }
        } 
        
        private Transform[] brackets = {};

        private void Awake()
        {
            var list = brackets.AsEnumerable();
            list = transform.Cast<Transform>().Aggregate(list, (current, transform) => current.Append(transform));
            brackets = list.ToArray();
        }

        private void OnEnable()
        {
            _changedBracketsDistance += ChangeSpacing;
        }

        private void OnDisable()
        {
            _changedBracketsDistance -= ChangeSpacing;
        }

        private void ChangeSpacing(float dist)
        {
            if (Mathf.Approximately(dist, -1))
            {
                foreach (var b in brackets)
                    b.gameObject.SetActive(false);
                
                return;
            }
            foreach (var b in brackets)
                b.gameObject.SetActive(true);
            spacing = dist + animationForce;
            this.DOKill(true);
            DOTween.To(() => spacing, f => spacing = f, dist, duration).SetTarget(this);
        }

        public float duration;
        public float animationForce;
        public Ease ease;
        
        public float spacing = 2f;
        public float spacingMultiplier = .5f;

        private void Update()
        {
            transform.position = HoverSelectionDestination;            
            brackets[0].localPosition = new Vector3(-spacing, spacing) * spacingMultiplier;
            brackets[1].localPosition = new Vector3(spacing, spacing) * spacingMultiplier;
            brackets[2].localPosition = new Vector3(-spacing, -spacing) * spacingMultiplier;
            brackets[3].localPosition = new Vector3(spacing, -spacing) * spacingMultiplier;
        }
    }
}