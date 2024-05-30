using System;
using System.Threading;
using System.Threading.Tasks;
using Eflatun.SceneReference;
using Muvuca.Core;
using Muvuca.Systems;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Muvuca.Game.Player
{
    public class PlayerLives : MonoBehaviour
    {
        public static int LifeCount;

        [SerializeField] private int startingLives;
        [SerializeField] private SceneReference gameOverScene;

        private void Awake()
        {
            LifeCount = SaveSystem.GetInt(SaveSystemKeyNames.PlayerLives) ?? startingLives;
        }

        private void OnEnable()
        {
            LifeRemoved += OnLifeRemoved;
        }

        private void OnDisable()
        {
            LifeRemoved -= OnLifeRemoved;
        }

        private void OnLifeRemoved()
        {
            LifeCount--;
            SaveSystem.Set(SaveSystemKeyNames.PlayerLives, LifeCount);

            if (LifeCount < 0)
            {
                LifeCount = startingLives;
                SaveSystem.Set(SaveSystemKeyNames.PlayerLives, LifeCount);
                SceneManager.LoadScene(gameOverScene.BuildIndex);
            }
            
            new Thread(() => SaveSystem.SaveToDisk(SaveSystem.SaveData)).Start();
        }

        public static event Action LifeRemoved;

        public static void RemoveLife() => LifeRemoved?.Invoke();
    }
}