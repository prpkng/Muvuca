using System;
using Muvuca.Core;
using Unity.VisualScripting;
using UnityEngine;

namespace Muvuca.Game.Player
{
    public class PlayerRespawner : MonoBehaviour
    {
        private void OnEnable()
        {
            InputManager.ResetPressed += Respawn;
        }

        private void OnDisable()
        {
            InputManager.ResetPressed -= Respawn;
        }

        public static Vector2 RespawnPosition;

        public void Respawn()
        {
            PlayerController.Instance.machine.ChangeState("moving", new string[] { });
            PlayerController.Instance.isInvulnerable = false;
            transform.position = RespawnPosition;
            LevelManager.Reset();
        }
    }
}