using System;
using Muvuca.Game.Player;
using UnityEngine;

namespace Muvuca.Game.Elements.Platform
{
    public class CheckpointPlatform : HitboxListener
    {
        public static event Action PlayerEnteredPlatform;
        protected override void Entered()
        {
            PlayerEnteredPlatform?.Invoke();
            PlayerRespawner.RespawnPosition = transform.position;
        }
    }
}