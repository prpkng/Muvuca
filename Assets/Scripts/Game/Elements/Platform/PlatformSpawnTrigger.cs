namespace Muvuca.Game.Elements.Platform
{
    using Muvuca.Core;
    using Muvuca.Game.Player;
    using UnityEngine;

    public class PlatformSpawnTrigger : MonoBehaviour
    {

        public void Trigger()
        {
            PlatformSpawner.SpawnNewPlatform();
        }
    }
}