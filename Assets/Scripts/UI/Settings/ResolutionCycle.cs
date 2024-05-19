namespace Muvuca.UI.Settings
{
    using System.Linq;
    using TMPro;
    using UnityEngine;
    using UnityEngine.EventSystems;

    public class ResolutionCycle : MonoBehaviour
    {
        private static Resolution gameStartResolution;

        public static void SetGameStartResolution()
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
            currentResolution = Screen.currentResolution;
            var currentSize = new Vector2(currentResolution.width, currentResolution.height);
            var resolutions = Screen.resolutions;
            var currentDistance = Mathf.Infinity;
            for (var i = 0; i < resolutions.Length; i++)
            {
                var item = resolutions[i];
                var resSize = new Vector2(item.width, item.height);
                var dist = Vector2.Distance(resSize, currentSize);
                if (dist >= currentDistance)
                    continue;
                currentDistance = dist;
                currentResolutionIndex = i;
            }
            
            resolutionsText.text = $"{currentResolution.width}x{currentResolution.height}";
            print(resolutions[currentResolutionIndex]);
        }
        private int currentResolutionIndex;
        private Resolution currentResolution;



        
        public void IncreaseDecreaseRes(int by)
        {
            var resolutions = Screen.resolutions;

            currentResolutionIndex += by;
            if (currentResolutionIndex >= resolutions.Length)
                currentResolutionIndex = 0;
            else if (currentResolutionIndex < 0)
                currentResolutionIndex = resolutions.Length - 1;
            
            currentResolution = resolutions[currentResolutionIndex];
            print($"Switching to resolution: {currentResolution}");

            applier.AddSetting(() =>
            {
                Screen.SetResolution(currentResolution.width, currentResolution.height, Screen.fullScreenMode);
                PlayerPrefs.SetInt("WindowWidth", currentResolution.width);
                PlayerPrefs.SetInt("WindowHeigth", currentResolution.height);

            }, typeof(ResolutionCycle));


            resolutionsText.text = $"{currentResolution.width}x{currentResolution.height}";
        }
    }
}