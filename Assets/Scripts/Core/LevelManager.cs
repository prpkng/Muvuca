using System;
using System.Collections.Generic;
using System.Linq;
using Muvuca.Game.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Muvuca.Core
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance;
        private void Awake()
        {
            if (Instance != null) Debug.LogError("You can't have multiple Level Managers in one single scene!");
            Instance = this;
            print(onLevelReset?.GetInvocationList().Length);
            onLevelReset = null;
        }

        public static event Action onLevelReset;

        public static void Reset(bool fillPlayerHp = true)
        {
            if (fillPlayerHp) PlayerController.Instance.health.currentHp = 5; // Change this later ( to a constant maybe? )
            PlayerController.PlayerHealthChanged?.Invoke();
            onLevelReset?.Invoke();
            onLevelReset = null;
        }
    }
}