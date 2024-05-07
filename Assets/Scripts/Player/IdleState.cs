using Muvuca.Effects;
using Muvuca.Elements;
using Muvuca.Input;
using Muvuca.Systems;
using System.Collections.Generic;
using UnityEngine;
namespace Muvuca.Player
{
    public class IdleState : State
    {

        public override void Enter(string[] data = null)
        {
            ((PlayerController)machine.owner).lineRenderer.enabled = true;
            InputManager.JumpPressed += JumpPressed;
            CameraBPM.TriggerBeat();

        }

        public void JumpPressed()
        {
            var owner = (PlayerController)machine.owner;
            machine.ChangeState("moving",
                new string[] { Util.SerializeVector3Array(new Vector3[] { owner.platform.up }) });

            if (owner.platform.TryGetComponent(out FixedPlatform plat))
                plat.hasPlayer = false;
        }


        public override void Update()
        {
            var owner = (PlayerController)machine.owner;
            owner.transform.up = owner.platform.up;
            owner.transform.position = owner.platform.position;
        }

        public override void Exit()
        {
            var owner = (PlayerController)machine.owner;
            owner.lineRenderer.enabled = false;

            if (owner.platform.TryGetComponent(out FixedPlatform plat)) plat.Disable();
            InputManager.JumpPressed -= JumpPressed;
            CameraBPM.TriggerBeat();

        }
    }
}