using System;
using Muvuca.Core;
using Muvuca.Game.Player;
using UnityEngine;

namespace Muvuca.Game.Elements.Platform
{
    public class PlatformSpawner : MonoBehaviour
    {
        public static void SpawnNewPlatform() => SpawnPlatform?.Invoke();
        public static event Action SpawnPlatform;

        public GameObject platformPrefab;

        private void OnEnable()
        {
            SpawnPlatform += PlatformSpawned;
        }

        private void OnDisable()
        {
            SpawnPlatform -= PlatformSpawned;
        }

        public static Transform currentCheckpointPlatform;

        public void PlatformSpawned()
        {
            var plat = Instantiate(platformPrefab);
            plat.transform.position = transform.position;
            PlayerController.Instance.machine.ChangeState("moving", new string[] { });
            PlayerController.Instance.transform.position = currentCheckpointPlatform.position;
            if (!PlayerController.Instance.isInvulnerable)
                PlayerController.Instance.DamagePlayer(1);
            LevelManager.Reset(false);
        }
    }
}