using System;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.SceneManagement;
using STOP_MODE = FMOD.Studio.STOP_MODE;

namespace Muvuca.Systems
{
    public class MainMenuController : MonoBehaviour
    {
        private static EventInstance? _mainMenuBGMEvent = null;
        private static bool _isPlayingMenuBGM;
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
        public static void PlayBGM()
        {
            #if UNITY_EDITOR
            if (SceneManager.GetActiveScene().name != "Main Menu")
                return;
            #endif
            _isPlayingMenuBGM = true;
            _mainMenuBGMEvent = RuntimeManager.CreateInstance("event:/Songs/Kmillo_Menu");
            _mainMenuBGMEvent?.start();
            RuntimeManager.StudioSystem.update();
        }

        
        private static float _bgmMuffle;

        public static float BGMMuffle
        {
            get => _bgmMuffle;
            set
            {
                _bgmMuffle = value;
                _mainMenuBGMEvent?.setParameterByName("Muffle", _bgmMuffle);
            }
        }

        private void Awake()
        {
            if (_isPlayingMenuBGM)
                return;
            
            PlayBGM();
            _isPlayingMenuBGM = true;
            
            if (_mainMenuBGMEvent != null)
                RuntimeManager.AttachInstanceToGameObject(_mainMenuBGMEvent.Value, transform);
        }

        private void OnDisable()
        {
            _mainMenuBGMEvent?.stop(STOP_MODE.IMMEDIATE);
            RuntimeManager.StudioSystem.update();
            _isPlayingMenuBGM = false;
        }
    }
}
