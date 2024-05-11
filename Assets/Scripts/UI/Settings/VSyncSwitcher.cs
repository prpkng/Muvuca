using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Muvuca.UI.Settings
{
    public class VSyncSwitcher : MonoBehaviour
    {
        public static bool VSyncEnabled;


        public string onText = "";
        public void SetOnText(string x) { onText = x; UpdateText(); } 
        public string offText = "";
        public void SetOffText(string x) { offText = x; UpdateText(); } 
        
        private void OnEnable()
        {
            VSyncEnabled = QualitySettings.vSyncCount != 0;
            UpdateText();
        }

        [SerializeField] private SettingsApplier applier;
        private TMP_Text tmp;
        private void Awake() => tmp = GetComponent<TMP_Text>();

        public void UpdateText()
        {
            if (tmp is null) return;
            tmp.text = VSyncEnabled ? onText : offText;
        }

        public void ToggleVSync()
        {
            VSyncEnabled = !VSyncEnabled;
                
            UpdateText();
            
            applier.AddSetting(() =>
            {
                QualitySettings.vSyncCount = VSyncEnabled ? 1 : 0;
            }, typeof(VSyncSwitcher));
            
        }
    }
}
