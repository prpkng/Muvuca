using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Muvuca.Elements
{
    public class LaunchPlatform : MonoBehaviour
    {
        public bool hasPlayer;
        
        public float rotateSpeed = 24f;
        private void DrawGizmoToAngle(float angle) =>
            Gizmos.DrawLine(transform.position, transform.position + (Vector3)
                new Vector2(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle)));
        private void DrawLineToAngle(float angle, Color color) =>
            Debug.DrawLine(transform.position, transform.position + (Vector3)
                new Vector2(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle)) * 100, color);
        
        
        private UnityEngine.Camera mainCamera;
        
        [Space]
        [SerializeField] private LayerMask platformLayer;

        public float aimAssistRange = 20f;
        public float lockForce = 0.75f;

        private void Awake()
        {
            mainCamera = UnityEngine.Camera.main;
        }


        private void Update()
        {
            if (!hasPlayer) return;
            var mousePos = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            var mouseDir = (mousePos - transform.position).normalized;
            var mouseAngle = Mathf.Rad2Deg * Mathf.Atan2(mouseDir.y, mouseDir.x);


            var t = (Vector2.Distance(mousePos, transform.position) / 20f);
            var range = Mathf.Lerp(aimAssistRange, 0, t);

            for (int i = 0; i < 5; i++)
            {
                var lineAngle = Mathf.LerpAngle(mouseAngle - range, mouseAngle + range, i / 5f) * Mathf.Deg2Rad;

                var lineDir = new Vector2(Mathf.Cos(lineAngle), Mathf.Sin(lineAngle));

                var hit = Physics2D.Raycast(transform.position + (Vector3)lineDir*2, lineDir,
                    16f, platformLayer);

                Debug.DrawRay(transform.position + (Vector3)lineDir, lineDir, hit ? Color.green : Color.red);
                if (!hit) continue;
                Debug.DrawLine(hit.point, hit.point + hit.normal, Color.magenta);
                
                
                mouseDir = (hit.transform.position - transform.position).normalized;
                mouseAngle = Mathf.LerpAngle(mouseAngle, Mathf.Rad2Deg * Mathf.Atan2(mouseDir.y, mouseDir.x), lockForce);
                break;
            }
            
            
            var euler = transform.eulerAngles;
            euler.z = Mathf.LerpAngle(euler.z, mouseAngle - 90f, Time.deltaTime * rotateSpeed);
            transform.eulerAngles = euler;
        }
        
        
    }
}