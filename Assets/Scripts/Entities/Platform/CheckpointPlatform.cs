using Muvuca.Core;
using Muvuca.Systems;
using UnityEngine;

namespace Muvuca.Entities.Platform
{
    public class CheckpointPlatform : MonoBehaviour
    {
        [SerializeField] private LaunchPlatform platform;

        private bool hadPlayer;

        private void Update()
        {
            if (!platform.hasPlayer || hadPlayer)
                return;
            
            SaveSystem.Set(SaveSystemKeyNames.PlayerSpawnPos, Util.SerializeVector2(transform.position));
            SaveSystem.SaveToDisk();
            
            hadPlayer = platform.hasPlayer;
        }
    }
}