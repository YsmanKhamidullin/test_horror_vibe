using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Core.Architecture.Services.Base;
using Game.Localization.Scripts;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace Game.Core.Architecture.Services
{
    public class SettingService : BaseService
    {
        public SettingsInfo SettingsInfo = new();
        public List<Locale> InterfaceLocales => LocalizationSettings.Instance.GetAvailableLocales().Locales;
        private static AudioMixer _masterMixer => Resources.Load<AudioMixer>("AudioClip/AudioMixer");

        public override UniTask Initialize()
        {
            _masterMixer.SetFloat("Volume", SettingsInfo.SoundVolume);

            return UniTask.CompletedTask;
        }

        public void ChangeInterfaceLanguage(Locale locale)
        {
            LocalizationWrapper.SetLocale(locale);
            SettingsInfo.InterfaceLanguage = locale.Identifier.Code;
        }

        public void ChangeSound(float arg0)
        {
            float v = arg0 > 0 ? Mathf.Log10(arg0) * 20 : -80;
            _masterMixer.SetFloat("SoundVolume", v);
        }
    }


    public class SettingsInfo
    {
        public int FrameRate = 60;
        public float SoundVolume = 0.5f;
        public string InterfaceLanguage = "en";
    }
}