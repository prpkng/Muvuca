namespace Muvuca.UI.Settings
{
    using System.Linq;
    using TMPro;
    using UnityEngine;
    using UnityEngine.EventSystems;

    public class ResolutionCycle : MonoBehaviour
    {
        private static Resolution gameStartResolution;
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
        private static void SetGameStartResolution()
        {
            gameStartResolution = Screen.currentResolution;

            if (!PlayerPrefs.HasKey("WindowWidth") || !PlayerPrefs.HasKey("WindowHeight"))
                return;
            Screen.SetResolution(PlayerPrefs.GetInt("WindowWidth"), PlayerPrefs.GetInt("WindowHeight"), Screen.fullScreenMode);
        }

        public SettingsApplier applier;
        private TMP_Text resolutionsText;

        private void Awake()
        {
            resolutionsText = GetComponent<TMP_Text>();
        }
        private int currentResolution;



        public void IncreaseDecreaseRes(int by)
        {
            currentResolution += by;
            var resolutions = Screen.resolutions.Where(r => r.refreshRateRatio.value == gameStartResolution.refreshRateRatio.value).ToArray();
            if (currentResolution >= resolutions.Length)
                currentResolution = 0;
            else if (currentResolution < 0)
                currentResolution = resolutions.Length - 1;

            var currentRes = resolutions[currentResolution];

            applier.AddSetting(() =>
            {
                Screen.SetResolution(currentRes.width, currentRes.height, Screen.fullScreenMode);
                PlayerPrefs.SetInt("WindowWidth", currentRes.width);
                PlayerPrefs.SetInt("WindowHeigth", currentRes.height);

            }, typeof(ResolutionCycle));


            resolutionsText.text = $"{currentRes.width}x{currentRes.height}";
        }
    }
}