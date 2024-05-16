using System;
using Muvuca.UI.HUD;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Muvuca.Entities.Platform
{
    public class LaunchPlatform : MonoBehaviour
    {
        public bool hasPlayer;
        private bool hadPlayer;
        
        public float rotateSpeed = 24f;
        private void DrawGizmoToAngle(float angle) =>
            Gizmos.DrawLine(transform.position, transform.position + (Vector3)
                new Vector2(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle)));
        private void DrawLineToAngle(float angle, Color color) =>
            Debug.DrawLine(transform.position, transform.position + (Vector3)
                new Vector2(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle)) * 100, color);
        
        
        private Camera mainCamera;
        
        [Space]
        [SerializeField] private LayerMask platformLayer;

        public float aimAssistRange = 20f;
        public float lockForce = 0.75f;

        public float selectionRange = 10f;
        
        private void Awake()
        {
            mainCamera = Camera.main;
        }

        private void OnDrawGizmos()
        {
            if (!hasPlayer) return;
            Gizmos.color = Color.green;
            Gizmos.DrawSphere((Vector2)mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue()),
                selectionRange);
        }


        private void Update()
        {
            if (!hasPlayer)
            {
                hadPlayer = hasPlayer;
                return;
            }
            var mousePos = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            var mouseDir = (mousePos - transform.position).normalized;
            var mouseAngle = Mathf.Rad2Deg * Mathf.Atan2(mouseDir.y, mouseDir.x);


            var hit = Physics2D.CircleCast(mousePos, selectionRange,
                Vector2.up, .01f, platformLayer);

            if (hit && Vector2.Distance(mousePos, transform.position) > selectionRange+1f)
            {
                HoverSelectionBracket.HoverSelectionDestination = hit.transform.position;
                HoverSelectionBracket.BracketsDistance =
                    Mathf.Max(hit.collider.bounds.size.x, hit.collider.bounds.size.y);
                mouseDir = (hit.transform.position - transform.position).normalized;
                mouseAngle = Mathf.LerpAngle(mouseAngle, Mathf.Rad2Deg * Mathf.Atan2(mouseDir.y, mouseDir.x),
                    lockForce);
            }
            else
                HoverSelectionBracket.BracketsDistance = -1;
            
            var euler = transform.eulerAngles;
            euler.z = Mathf.LerpAngle(euler.z, mouseAngle - 90f, hadPlayer ? Time.deltaTime * rotateSpeed : 1);
            transform.eulerAngles = euler;
            
            hadPlayer = hasPlayer;
        }
        
        
    }
}