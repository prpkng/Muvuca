using System;
using TMPro;
using UnityEngine;

namespace Muvuca.UI.Settings
{
    public class FullscreenSwitcher : MonoBehaviour
    {
        public static FullScreenMode CurrentFullScreenMode;

        public void SetFullscreenText(string x) { fullscreenText = x; UpdateText(); }
        public string fullscreenText;
        
        private string FullscreenModeToString(FullScreenMode mode)
        {
            var split = fullscreenText.Split('|');
            return mode switch
            {
                FullScreenMode.ExclusiveFullScreen => split[1],
                FullScreenMode.FullScreenWindow => split[0],
                FullScreenMode.MaximizedWindow => split[0],
                FullScreenMode.Windowed => split[2],
                _ => "ERROR"
            };
        }
        
        
        private void OnEnable()
        {
            CurrentFullScreenMode = Screen.fullScreenMode;
        }

        private void Start()
        {
            UpdateText();
        }

        [SerializeField] private SettingsApplier applier;
        private TMP_Text tmp;
        private void Awake() => tmp = GetComponent<TMP_Text>();


        public void UpdateText()
        {
            tmp.text = FullscreenModeToString(CurrentFullScreenMode);
        }
        
        public void ToggleFullscreen()
        {
            CurrentFullScreenMode = CurrentFullScreenMode switch
            {
                FullScreenMode.ExclusiveFullScreen => FullScreenMode.Windowed,
                FullScreenMode.FullScreenWindow => FullScreenMode.ExclusiveFullScreen,
                FullScreenMode.MaximizedWindow => FullScreenMode.Windowed,
                FullScreenMode.Windowed => FullScreenMode.FullScreenWindow,
                _ => throw new ArgumentOutOfRangeException()
            };

            UpdateText();
            
            applier.AddSetting(() =>
            {
                Screen.fullScreenMode = CurrentFullScreenMode;
            }, typeof(FullscreenSwitcher));
            
        }
    }
}
