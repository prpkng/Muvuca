using Muvuca.Core;
using Muvuca.Game.Elements.Platform;
using Muvuca.Systems;
using Muvuca.UI.HUD;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Muvuca.Game.Player
{
    public class IdleState : PlayerState
    {
        private Camera cam;
        public override void Enter(string[] data = null)
        {
            cam = Camera.main;
            player.lineRenderer.gameObject.SetActive(true);
            player.hasPlatform = true;
            InputManager.JumpPressed += JumpPressed;
            player.PlayAnimation("land");
            frameCount = 0;
            overrideDirection = null;
            player.flipping.enabled = true;
        }

        private Vector3? overrideDirection = null;
        
        public void JumpPressed()
        {
            player.PlayAnimation("jump");

            Vector2 dir = overrideDirection ??
                          PlatformSelector.Instance.targetPosition - player.transform.position;
            dir = dir.normalized;
            
            machine.ChangeState("moving",
                new[] { Util.SerializeVector3Array(new[] { (Vector3)dir }) });

            player.transform.up = dir;
            
            if (player.platform.TryGetComponent(out LaunchPlatform plat))
                plat.hasPlayer = false;
        }

        private int frameCount;
        
        public override void Update()
        {
            player.transform.up = player.platform.up;
            player.transform.position = player.platform.position;

            frameCount++;
            
            if (PlayerInputBuffering.BufferedPosition == null 
                || Vector2.Distance(player.transform.position, PlayerInputBuffering.BufferedPosition.Value) < 1.5f 
                || frameCount < 3) return;
            overrideDirection = (PlayerInputBuffering.BufferedPosition.Value - player.transform.position).normalized;
            JumpPressed();
            PlayerInputBuffering.BufferedPosition = null;
        }

        public override void Exit()
        {
            player.flipping.enabled = false;
            HoverSelectionBracket.BracketsDistance = -1;
            player.lineRenderer.gameObject.SetActive(false);
            player.hasPlatform = false;
            
            player.jumpSound.Play();

            InputManager.JumpPressed -= JumpPressed;
        }
    }
}