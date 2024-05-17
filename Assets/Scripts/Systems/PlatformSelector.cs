using System;
using Muvuca.Core;
using Muvuca.Tools;
using Muvuca.UI.HUD;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Muvuca.Systems
{
    public class PlatformSelector : MonoBehaviour
    {
        public static PlatformSelector Instance;
        private Camera mainCamera;
        
        [ReadOnly] public Vector3 targetPosition;
        
        [SerializeField] private LayerMask platformLayer;

        public float lockForce = 0.75f;

        public float selectionRange = 10f;
        
        private void Awake()
        {
            mainCamera = Camera.main;
            Instance = this;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(targetPosition,
                selectionRange);
        }

        private void Update()
        {
            targetPosition = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            var hit = Physics2D.CircleCast(targetPosition, selectionRange,
                Vector2.up, .001f, platformLayer);

            if (hit && Vector2.Distance(targetPosition, PlayerController.Instance.transform.position) > selectionRange+1f)
            {
                HoverSelectionBracket.HoverSelectionDestination = hit.transform.position;
                HoverSelectionBracket.BracketsDistance =
                    Mathf.Max(hit.collider.bounds.size.x, hit.collider.bounds.size.y);
                
                targetPosition = Vector3.Lerp(hit.transform.position, targetPosition, lockForce);
            }
            else
                HoverSelectionBracket.BracketsDistance = -1;

            targetPosition.z = 0;
        }
    }
}