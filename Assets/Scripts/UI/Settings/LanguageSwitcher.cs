namespace Muvuca.UI.Settings
{
    using TMPro;
    using UnityEngine;
    using UnityEngine.Localization.Settings;

    public class LanguageSwitcher : MonoBehaviour
    {
        private int selectedLocaleIndex;
        private string selectedLocale;

        public int IdToLocaleIndex(string locale)
        {
            return locale switch
            {
                "en" => 0,
                "pt" => 1,
                "es" => 2,
                "jp" => 3,
                "fr" => 4,
                _ => 0,
            };
        }
        public string IndexToLocaleId(int index)
        {
            return index switch
            {
                0 => "en",
                1 => "pt",
                2 => "es",
                3 => "jp",
                4 => "fr",
                _ => "en",
            };
        }

        public void ToggleLocale()
        {
            ChangeLocale(IndexToLocaleId(selectedLocaleIndex + 1));
        }

        public void ChangeLocale(string newLocale)
        {
            selectedLocaleIndex = IdToLocaleIndex(newLocale);
            selectedLocale = IndexToLocaleId(selectedLocaleIndex);
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[selectedLocaleIndex];
        }
    }
}