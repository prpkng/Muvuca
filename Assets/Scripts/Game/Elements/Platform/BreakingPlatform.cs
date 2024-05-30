using System;
using Cysharp.Threading.Tasks;
using Muvuca.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Muvuca.Game.Elements.Platform
{
    public class BreakingPlatform : HitboxListener
    {
        [SerializeField] private LaunchPlatform platform;
        [SerializeField] private float duration;

        [SerializeField] private UnityEvent onBreak;
        [SerializeField] private UnityEvent onReset;

        private bool broken;

        private void Awake()
        {
            CheckpointPlatform.PlayerEnteredPlatform += Fix;
        }


        private void OnDestroy()
        {
            CheckpointPlatform.PlayerEnteredPlatform -= Fix;
        }

        private void Fix()
        {
            if (!broken) return;
            broken = false;
            onReset.Invoke();
        }


        protected override async void Entered()
        {
            await UniTask.WaitForSeconds(duration);
            onBreak.Invoke();
            broken = true;
            if (!platform.hasPlayer) return;
            
            PlayerController.Instance.DamagePlayer();
            PlayerController.Instance.machine.ChangeState("return");
        }
    }
}