using Muvuca.Player;
using System.Collections.Generic;
using UnityEngine;

namespace Muvuca.Systems
{
    public enum Element
    {
        Neutral,
        Fire,
        Water,
        Earth,
        Air
    }
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance;
        private void Awake()
        {
            if (Instance != null) Debug.LogError("You can't have multiple Level Managers in one single scene!");
            Instance = this;
        }

        public Element activeElement = Element.Neutral;

        public Transform startingPlatform;
        public List<IEnablable> disabledElements = new();

        public static void Reset()
        {
            print("Reset");
            PlayerController.Instance.Disable();
            foreach (var item in Instance.disabledElements)
            {
                item.Enable();
            }
        }
    }
}