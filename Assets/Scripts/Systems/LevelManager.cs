using Muvuca.Player;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Muvuca.Systems
{
    public enum Element
    {
        Neutral,
        Fire,
    }
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance;
        private void Awake()
        {
            if (Instance != null) Debug.LogError("You can't have multiple Level Managers in one single scene!");
            Instance = this;
            Instance.activeElement = Element.Neutral;
        }

        public Element activeElement = Element.Neutral;

        public static void Reset()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}