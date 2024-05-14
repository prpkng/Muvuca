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
        
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void SetupVolume()
        {
            _masterBus = FMODUnity.RuntimeManager.GetBus("bus:/");
            _sfxBus = FMODUnity.RuntimeManager.GetBus("bus:/SFX");
            _musicBus = FMODUnity.RuntimeManager.GetBus("bus:/Music");
            
            if (PlayerPrefs.HasKey("GeneralVolume"))
                _masterBus.setVolume(PlayerPrefs.GetFloat("GeneralVolume"));    
            else
                _SetGeneralVolume(0.5f);
            if (PlayerPrefs.HasKey("SfxVolume"))
                _sfxBus.setVolume(PlayerPrefs.GetFloat("SfxVolume"));
            else
                _SetSfxVolume(0.5f);
            if (PlayerPrefs.HasKey("MusicVolume"))
                _musicBus.setVolume(PlayerPrefs.GetFloat("MusicVolume"));
            else
                _SetMusicVolume(0.5f);
        }

        public static void _SetGeneralVolume(float v) 
        {
            PlayerPrefs.SetFloat("GeneralVolume", v);
            _masterBus.setVolume(v);
        }
        public static void _SetSfxVolume(float v) 
        {
            PlayerPrefs.SetFloat("SfxVolume", v);
            _sfxBus.setVolume(v);
        }
        public static void _SetMusicVolume(float v) 
        {
            PlayerPrefs.SetFloat("MusicVolume", v);
            _musicBus.setVolume(v);
        }
    }
}