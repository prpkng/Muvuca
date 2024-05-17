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
            InputManager.JumpPressed += JumpPressed;
            Debug.Log("Entered idle");
            player.PlayAnimation("idle");
        }

        public void JumpPressed()
        {
            player.PlayAnimation("jump");

            machine.ChangeState("moving",
                new[] { Util.SerializeVector3Array(new[] { player.platform.up }) });

            if (player.platform.TryGetComponent(out LaunchPlatform plat))
                plat.hasPlayer = false;
        }


        public override void Update()
        {
            player.transform.up = player.platform.up;
            player.transform.position = player.platform.position;
        }

        public override void Exit()
        {
            HoverSelectionBracket.BracketsDistance = -1;
            player.lineRenderer.enabled = false;

            InputManager.JumpPressed -= JumpPressed;
        }
    }
}