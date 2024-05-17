using System;
using Muvuca.Core;
using Muvuca.Player;
using Muvuca.Systems;
using Muvuca.UI.HUD;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Muvuca.Entities.Platform
{
    public class LaunchPlatform : MonoBehaviour
    {
        public bool hasPlayer;

        private void Update()
        {
            if (!hasPlayer) return;
            
            
            var targetPos = PlayerInputBuffering.BufferedPosition ?? PlatformSelector.Instance.targetPosition;
            var targetDir = (PlatformSelector.Instance.targetPosition - transform.position).normalized;
            var angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90);
            
            if (PlayerInputBuffering.BufferedPosition != null)
            {
                InputManager.JumpPressed?.Invoke();
                PlayerInputBuffering.BufferedPosition = null;
            }
        }
    }
}