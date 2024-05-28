using Muvuca.Game.Player;
using UnityEngine;

namespace Muvuca.Game.Elements.Platform
{
    public class CheckpointPlatform : HitboxListener
    {
        protected override void Entered()
        {
            PlayerRespawner.RespawnPosition = transform.position;
        }
    }
}