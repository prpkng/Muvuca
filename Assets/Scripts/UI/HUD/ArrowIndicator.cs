using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Muvuca.UI.HUD
{
    public class ArrowIndicator : MonoBehaviour
    {
        private float Height => cam.orthographicSize - padding;
        private float Width => cam.orthographicSize / 9f * 16f - padding ;
        [SerializeField] private float targetDistance;
        [SerializeField] private float padding = 2f;
        public static Vector2? Target;

        private Camera cam;

        private void Awake()
        {
            cam = Camera.main;
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
            
            transform.position = (Vector3)Target.Value + (Vector3)(((Vector2)transform.parent.position - Target.Value)
                                                                   .normalized
                                                                   * targetDistance);
            var localPos = transform.localPosition;
            localPos.x = Mathf.Clamp(localPos.x, -Width / 2, Width / 2);
            localPos.y = Mathf.Clamp(localPos.y, -Height / 2, Height / 2);
            localPos.z = 0;
            transform.localPosition = localPos;
            transform.up = (new Vector3(Target.Value.x, Target.Value.y, transform.position.z) - transform.position).normalized;
        }
    }
}