using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Settings;

namespace Game.Localization.Scripts
{
    public class TMPLocalizedFont : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _label;

        [SerializeField]
        private LocalizeStringEvent _localizeStringEvent;

        private void OnEnable()
        {
            LocalizationSettings.Instance.OnSelectedLocaleChanged += UpdateFont;
            SetToCurrentLocale();
        }

        private void OnDisable()
        {
            LocalizationSettings.Instance.OnSelectedLocaleChanged -= UpdateFont;
        }

        private void UpdateFont(Locale obj)
        {
            SetToCurrentLocale();
        }

        private void SetToCurrentLocale()
        {
            _label.font = LocalizationWrapper.GetFontByCurrentLocale();
            if (_localizeStringEvent && _localizeStringEvent.enabled)
            {
                _localizeStringEvent.RefreshString();
            }
        }
    }
}