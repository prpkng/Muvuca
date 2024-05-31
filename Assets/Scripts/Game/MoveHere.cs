namespace Muvuca.Game
{
    using Muvuca.Core;
    using Muvuca.Game.Elements.Platform;
    using UnityEngine;

    public class MoveHere : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Transform RespawnPlatform;

        public void Move()
        {
            PlatformSpawner.currentCheckpointPlatform = RespawnPlatform;
            target.position = transform.position;
        }
    }
}