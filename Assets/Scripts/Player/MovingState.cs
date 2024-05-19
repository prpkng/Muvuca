using DG.Tweening;
using Muvuca.Effects;
using Muvuca.Systems;
using System;
using System.Collections.Generic;
using Muvuca.Core;
using Muvuca.Entities.Platform;
using UnityEngine;

namespace Muvuca.Player
{
    public class MovingState : PlayerState
    {

        private float counter;
        public Vector3 direction;

        public override void Enter(string[] data = null)
        {
            direction = data is { Length: <= 0 } ? Vector3.zero : Util.DeserializeVector3Array(data[0])[0];

            player.enteredPlatform += EnteredPlatform;

            InputManager.AttackPressed += AttackPressed;
            counter = 0;
        }

        private void AttackPressed()
        {
            player.PlayAnimation("attack");
        }

        public override void Exit()
        {
            player.enteredPlatform -= EnteredPlatform;
            InputManager.AttackPressed -= AttackPressed;
        }

        private void EnteredPlatform(Transform platform)
        {
            player.platform = platform;
            player.transform.position = platform.position;
            if (platform.TryGetComponent(out LaunchPlatform plat))
                plat.hasPlayer = true;

            machine.ChangeState("idle", null);
        }

        public override void Update()
        {
            counter += Time.deltaTime;
            var speed = player.distanceSpeedCurve.Evaluate(counter) * player.movingSpeed;
            if (speed <= 0)
            {
                machine.ChangeState("return");
                return;
            }
            player.transform.position += speed * Time.deltaTime * direction;
        }
    }
}