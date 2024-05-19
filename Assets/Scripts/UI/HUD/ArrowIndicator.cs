using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Muvuca.UI.HUD
{
    public class ArrowIndicator : MonoBehaviour
    {
        [SerializeField] private float width;
        [SerializeField] private float height;
        [SerializeField] private float targetDistance;
        public static Vector2? Target;

        private void Awake()
        {
            Target = null;
        }

        private void OnDrawGizmos() 
        {
            Gizmos.color = Color.green;
            if (Target.HasValue) Gizmos.DrawCube(Target.Value, Vector3.one);
        }

        private void Update()
        {
            transform.localScale = Vector3.one * (Target.HasValue ? 1 : 0);
            if (!Target.HasValue) return;
            
            transform.position = (Vector3)Target.Value + (transform.parent.position - (Vector3)Target.Value).normalized 
                * targetDistance;
            var localPos = transform.localPosition;
            localPos.x = Mathf.Clamp(localPos.x, -width / 2, width / 2);
            localPos.y = Mathf.Clamp(localPos.y, -height / 2, height / 2);
            transform.localPosition = localPos;
            transform.up = ((Vector3)Target.Value - transform.parent.position).normalized;
        }
    }
}