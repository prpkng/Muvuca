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
            player.lineRenderer.enabled = true;
            player.hasPlatform = true;
            InputManager.JumpPressed += JumpPressed;
            Debug.Log("Entered idle");
            player.PlayAnimation("land");
            frameCount = 0;

        }

        public void JumpPressed()
        {
            player.PlayAnimation("jump");

            machine.ChangeState("moving",
                new[] { Util.SerializeVector3Array(new[] { player.platform.up }) });

            if (player.platform.TryGetComponent(out LaunchPlatform plat))
                plat.hasPlayer = false;
        }

        private int frameCount;
        
        public override void Update()
        {
            player.transform.up = player.platform.up;
            player.transform.position = player.platform.position;

            frameCount++;
            
            if (PlayerInputBuffering.BufferedPosition == null || frameCount < 3) return;
            JumpPressed();
            PlayerInputBuffering.BufferedPosition = null;
        }

        public override void Exit()
        {
            HoverSelectionBracket.BracketsDistance = -1;
            player.lineRenderer.enabled = false;
            player.hasPlatform = false;

            InputManager.JumpPressed -= JumpPressed;
        }
    }
}