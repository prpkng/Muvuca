using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Muvuca.Elements
{
    public class LaunchPlatform : MonoBehaviour
    {
        public bool hasPlayer;
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + (Vector3)
                new Vector2(Mathf.Cos(Mathf.Deg2Rad * minimumAngle), Mathf.Sin(Mathf.Deg2Rad * minimumAngle)));
            Gizmos.DrawLine(transform.position, transform.position + (Vector3)
                new Vector2(Mathf.Cos(Mathf.Deg2Rad * maximumAngle), Mathf.Sin(Mathf.Deg2Rad * maximumAngle)));
        }

        public float minimumAngle;
        public float maximumAngle;

        public float rotateSpeed;
        
        private UnityEngine.Camera mainCamera;

        private void Awake()
        {
            mainCamera = UnityEngine.Camera.main;
        }

        private void Update()
        {
            if (!hasPlayer) return;
            var mousePos = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            var dir = (mousePos - transform.position).normalized;
            var angle = Util.ClampAngle(Mathf.Rad2Deg * Mathf.Atan2(dir.y, dir.x), minimumAngle, maximumAngle);

            transform.eulerAngles = Vector3.forward * (angle - 90f);

        }
    }
}