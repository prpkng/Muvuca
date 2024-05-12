using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Muvuca.Systems
{
    public class MainMenuController : MonoBehaviour
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
        public static void PlayBGM()
        {
            #if UNITY_EDITOR
            if (SceneManager.GetActiveScene().name != "Main Menu")
                return;
            #endif
            var eventInstance = RuntimeManager.CreateInstance("event:/Songs/Kmillo_Menu");
            eventInstance.start();
            eventInstance.release();
            RuntimeManager.StudioSystem.update();
        }
    }
}
