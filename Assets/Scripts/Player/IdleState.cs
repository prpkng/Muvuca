using Muvuca.Effects;
using Muvuca.Systems;
using System.Collections.Generic;
using Muvuca.Core;
using Muvuca.Entities.Platform;
using Muvuca.UI.HUD;
using UnityEngine;
namespace Muvuca.Player
{
    public class IdleState : PlayerState
    {

        public override void Enter(string[] data = null)
        {
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

            machine.ChangeState("moving",
                new[] { Util.SerializeVector3Array(new[] { overrideDirection ?? player.platform.up }) });

            player.transform.up = overrideDirection ?? player.platform.up;
            
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