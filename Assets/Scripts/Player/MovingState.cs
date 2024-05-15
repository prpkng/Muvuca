using DG.Tweening;
using Muvuca.Effects;
using Muvuca.Elements;
using Muvuca.Input;
using Muvuca.Systems;
using System;
using System.Collections.Generic;
using Muvuca.Elements.Platform;
using UnityEngine;

namespace Muvuca.Player
{
    public class MovingState : State
    {

        public Vector3 direction;

        public override void Enter(string[] data = null)
        {
            direction = data is { Length: <= 0 } ? Vector3.zero : Util.DeserializeVector3Array(data[0])[0];

            var owner = (PlayerController)machine.owner;
            owner.enteredPlatform += EnteredPlatform;

            InputManager.AttackPressed += AttackPressed;
        }

        private void AttackPressed()
        {
            var owner = (PlayerController)machine.owner;
            owner.transform.GetChild(0).localEulerAngles = Vector3.zero;
            owner.transform.GetChild(0).DOLocalRotate(-Vector3.forward * 360, .25f, RotateMode.LocalAxisAdd);
        }

        public override void Exit()
        {
            ((PlayerController)machine.owner).enteredPlatform -= EnteredPlatform;
            InputManager.AttackPressed -= AttackPressed;
        }

        private void EnteredPlatform(Transform platform)
        {
            var owner = (PlayerController)machine.owner;
            owner.platform = platform;
            owner.transform.position = platform.position;
            if (platform.TryGetComponent(out LaunchPlatform plat))
                plat.hasPlayer = true;

            machine.ChangeState("idle", null);
        }

        public override void Update()
        {
            var owner = (PlayerController)machine.owner;
            owner.transform.position += owner.movingSpeed * Time.deltaTime * direction;
        }
    }
}