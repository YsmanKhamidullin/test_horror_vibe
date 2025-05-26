using System;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace Game.Localization.Scripts
{
    [CreateAssetMenu(menuName = "Game/Create LocalizationFontsSo", fileName = "LocalizationFontsConfig", order = 0)]
    public class LocalizationFontsSo : ScriptableObject
    {
        public Locale EnglishLocale;

        [SerializeField]
        private List<LocaleFontPair> _localeFontPairs;

        [SerializeField]
        private TMP_FontAsset _defaultFont;

        public TMP_FontAsset GetByLocale(Locale locale)
        {
            return _localeFontPairs.First(l => l.Locale.Identifier.Code == locale.Identifier.Code).Font;
        }

        [Button]
        private void Reset()
        {
            var a = LocalizationSettings.AvailableLocales.Locales;
            _localeFontPairs = new List<LocaleFontPair>();
            foreach (var l in a)
            {
                var newPair = new LocaleFontPair
                {
                    Locale = l,
                    Font = _defaultFont
                };
                _localeFontPairs.Add(newPair);
            }
        }
    }

    [Serializable]
    public class LocaleFontPair
    {
        public Locale Locale;
        public TMP_FontAsset Font;
    }
}