using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace Game.Localization.Scripts
{
    public static class LocalizationWrapper
    {
        public static LocalizationFontsSo Config
        {
            get
            {
                if (_config == null)
                {
                    LoadConfigFromResources();
                }

                return _config;
            }
        }

        private static LocalizationFontsSo _config;

        public static void SetLocale(Locale locale)
        {
            LocalizationSettings.Instance.SetSelectedLocale(locale);
        }

        public static void SetDefaultLocale()
        {
            LocalizationSettings.Instance.SetSelectedLocale(GetEnglish());
        }

        public static TMP_FontAsset GetFontByCurrentLocale()
        {
            return GetFont(LocalizationSettings.Instance.GetSelectedLocale());
        }

        public static Locale GetEnglish()
        {
            return Config.EnglishLocale;
        }

        public static TMP_FontAsset GetFont(Locale locale)
        {
            return Config.GetByLocale(locale);
        }

        private static void LoadConfigFromResources()
        {
            _config = Resources.Load<LocalizationFontsSo>("LocalizationFontsConfig");
        }

        public static string Get(string key)
        {
            var text = LocalizationSettings.StringDatabase.GetLocalizedString(key);
            return text;
        }

        public static string GetLocalizationByKey(Locale locale, string key)
        {
            var text = LocalizationSettings.StringDatabase.GetLocalizedString(key, locale);
            return text;
        }

        public static string GetEnglish(string key)
        {
            return GetLocalizationByKey(GetEnglish(), key);
        }
    }
}