using Muvuca.Systems;
using Muvuca.Tools;
using Muvuca.UI.Menu;
using Muvuca.UI.Settings;
using UnityEngine;

namespace Muvuca.Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public static bool isMobilePlatform => Instance.isRunningOnMobile || Instance.runningOnMobileOverride;

        [ReadOnly] public bool isRunningOnMobile;
        public bool runningOnMobileOverride;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        public static void SetupGameManager()
        {
            Cursor.lockState = CursorLockMode.Confined;

            var gameManager = new GameObject("GameManager");
            Instance = gameManager.AddComponent<GameManager>();
            DontDestroyOnLoad(gameManager);

            VolumeControl.SetupFMODVolume();

            // Fix framerate cap
            Application.targetFrameRate = 0;

            // Disable saving for this build!!!!
            // SaveSystem.LoadFromDisk();

            ResolutionCycle.SetGameStartResolution();

            MainMenuController.PlayBGM();


            PlayerProgress = SaveSystem.GetInt(SaveSystemKeyNames.PlayerProgress) ?? 1;


        }



        public static int PlayerProgress = 1;


    }
}