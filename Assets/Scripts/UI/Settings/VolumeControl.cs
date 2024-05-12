using System;
using FMOD.Studio;
using UnityEngine;

namespace Muvuca.UI.Settings
{
    public class VolumeControl : MonoBehaviour
    {
        private static Bus _masterBus;
        private static Bus _sfxBus;
        private static Bus _musicBus;
        
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
        private static void SetupVolume()
        {
            _masterBus = FMODUnity.RuntimeManager.GetBus("bus:/");
            _sfxBus = FMODUnity.RuntimeManager.GetBus("bus:/SFX");
            _musicBus = FMODUnity.RuntimeManager.GetBus("bus:/Music");
            
            if (PlayerPrefs.HasKey("GeneralVolume"))
                _SetGeneralVolume(PlayerPrefs.GetFloat("GeneralVolume"));    
            if (PlayerPrefs.HasKey("SfxVolume"))
                _SetSfxVolume(PlayerPrefs.GetFloat("SfxVolume"));
            if (PlayerPrefs.HasKey("MusicVolume"))
                _SetMusicVolume(PlayerPrefs.GetFloat("MusicVolume"));
        }

        public void SetGeneralVolume(float v) => _SetGeneralVolume(v);
        public static void _SetGeneralVolume(float v) 
        {
            PlayerPrefs.SetFloat("GeneralVolume", v);
            _masterBus.setVolume(v);
        }
        public void SetSfxVolume(float v) => _SetSfxVolume(v);        
        public static void _SetSfxVolume(float v) 
        {
            PlayerPrefs.SetFloat("SfxVolume", v);
            _sfxBus.setVolume(v);
        }
        public void SetMusicVolume(float v) => _SetMusicVolume(v);        
        public static void _SetMusicVolume(float v) 
        {
            PlayerPrefs.SetFloat("MusicVolume", v);
            _musicBus.setVolume(v);
        }
    }
}