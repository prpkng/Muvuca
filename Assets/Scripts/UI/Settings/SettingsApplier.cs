namespace Muvuca.UI.Settings
{
    using UnityEngine;
    using System.Collections.Generic;
    using System;
    using System.Linq;
    using UnityEngine.UI;

    public class SettingsApplier : MonoBehaviour
    {
        public Dictionary<Type, Action> settingsToApply = new();

        [SerializeField] private Image applyButton;
        [SerializeField] private Color appliedColor;
        [SerializeField] private Color dirtyColor;

        public void AddSetting(Action action, Type type)
        {
            applyButton.color = dirtyColor;
            settingsToApply[type] = action;
        }

        public void Apply()
        {
            settingsToApply.Values.All(a => { a.Invoke(); return true; });
            settingsToApply.Clear();
            applyButton.color = appliedColor;
        }
    }
}