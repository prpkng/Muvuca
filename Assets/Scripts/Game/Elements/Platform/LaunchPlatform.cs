using System.Collections.Generic;
using Muvuca.Game.Player;
using Muvuca.Systems;
using UnityEngine;

namespace Muvuca.Game.Elements.Platform
{
    public class LaunchPlatform : MonoBehaviour
    {
        public bool isSafePlatform = true;
        public bool followMouse = true;
        public static List<LaunchPlatform> availablePlatforms = new();

        private void OnEnable() => availablePlatforms.Add(this);

        private void OnDisable() => availablePlatforms.Remove(this);

        public const float CameraLensOrthoSize = 14f;

        private void OnDrawGizmosSelected()
        {
            var camHalfWid = CameraLensOrthoSize / 9f * 16f;
            var position = transform.position;
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(position + new Vector3(-camHalfWid, -CameraLensOrthoSize), position + new Vector3(-camHalfWid, CameraLensOrthoSize));
            Gizmos.DrawLine(position + new Vector3(camHalfWid, -CameraLensOrthoSize), position + new Vector3(camHalfWid, CameraLensOrthoSize));
            Gizmos.DrawLine(position + new Vector3(-camHalfWid, -CameraLensOrthoSize), position + new Vector3(camHalfWid, -CameraLensOrthoSize));
            Gizmos.DrawLine(position + new Vector3(-camHalfWid, CameraLensOrthoSize), position + new Vector3(camHalfWid, CameraLensOrthoSize));
        }


        public bool hasPlayer;

        private void Update()
        {
            if (!hasPlayer || !followMouse) return;
            var targetDir = (PlatformSelector.Instance.targetPosition - transform.position).normalized;
            var angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        }
    }
}