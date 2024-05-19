using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Muvuca.Tools
{
    public class DevConsole : MonoBehaviour
    {
        private const string DevConsolePath = "Assets/Game/Utils/DevConsole.prefab";
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static async void SpawnDevConsole()
        {
            if (!Application.isEditor && !Application.isPlaying && !Debug.isDebugBuild) return;
            var devConsoleHandle = Addressables.LoadAssetAsync<GameObject>(DevConsolePath);
            print("Loading dev console...");
            await devConsoleHandle.Task;
            print("Dev console loaded!");

            var devConsole = Instantiate(devConsoleHandle.Result);
            DontDestroyOnLoad(devConsole);
        }
        
        // ==== Mono Behaviour area
        [SerializeField] private GameObject labelObject;
        [SerializeField] private Transform activeContainer;
        [SerializeField] private Transform hiddenContainer;
        [SerializeField] private float fadeDuration;
        [SerializeField] private Ease fadeEase;
        
        private Queue<TMP_Text> idleLabels = new();
        private List<TMP_Text> activeLabels = new();

        private TMP_Text PickLabel()
        {
            if (idleLabels.Count > 0)
                return idleLabels.Dequeue();
            
            var label = activeLabels[0];
            activeLabels.RemoveAt(0);
            label.DORewind();
            label.DOKill(true);
            return label;
        }

        private void Awake()
        {
            for (var i = 0; i < 25; i++) idleLabels.Enqueue(Instantiate(labelObject, hiddenContainer).GetComponent<TMP_Text>());
        }


        private void OnEnable()
        {
            Application.logMessageReceived += OnLogMessage;
        }

        private void OnLogMessage(string condition, string stacktrace, LogType type)
        {
            var label = PickLabel();
            activeLabels.Add(label);
            label.transform.SetParent(activeContainer);
            label.text = $"{condition}";

            var sizeDelta = ((RectTransform)label.transform).sizeDelta;
            sizeDelta.y = label.GetPreferredValues().y;
            ((RectTransform)label.transform).sizeDelta = sizeDelta;
            
            label.color = type switch
            {
                LogType.Error => Color.red,
                LogType.Warning => (Color.yellow + Color.red) / 2,
                LogType.Log => Color.white,
                LogType.Exception => (Color.red + Color.black) / 2f,
                _ => Color.white
            };

            label.DOFade(0f, fadeDuration).SetEase(fadeEase).onComplete += () =>
            {
                label.transform.SetParent(hiddenContainer);
                activeLabels.Remove(label);
                idleLabels.Enqueue(label);
            };
        }

        private void OnDisable()
        {
            Application.logMessageReceived -= OnLogMessage;
        }
    }
}