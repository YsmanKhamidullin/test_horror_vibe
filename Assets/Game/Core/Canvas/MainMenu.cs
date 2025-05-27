using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Core.Architecture;
using Game.Core.Architecture.Services;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

namespace Game.Core.Canvas
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField]
        private Button _chapterOneButton;

        [SerializeField]
        private Button _closeMenuButton;

        [SerializeField]
        private Button _exitGameButton;

        [SerializeField]
        private TMP_Dropdown _interfaceLanguageDropdown;

        [SerializeField]
        private Slider _soundSlider;

        private SettingService _settingService;
        private InputService _inputService;

        private async void Awake()
        {
            _inputService = await Project.Get<InputService>();
            _inputService.OnEsc += TryShowMenu;

            Initialize().Forget();
            _chapterOneButton.onClick.AddListener(LoadGame);
            _closeMenuButton.onClick.AddListener(CloseMenu);
            _exitGameButton.onClick.AddListener(ExitGame);

            if (SceneService.IsInGameScene())
            {
                _closeMenuButton.gameObject.SetActive(true);
                gameObject.SetActive(false);
            }
            else
            {
                _closeMenuButton.gameObject.SetActive(false);
            }
        }

        private void OnDestroy()
        {
            _inputService.OnEsc -= TryShowMenu;
        }

        private void ExitGame()
        {
            Application.Quit();
        }

        private void CloseMenu()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            gameObject.SetActive(false);
        }

        private void TryShowMenu()
        {
            if (SceneService.IsInGameScene())
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                gameObject.SetActive(true);
            }
        }

        private void LoadGame()
        {
            SceneService.LoadGameScene();
        }

        public async UniTask Initialize()
        {
            _settingService = await Project.Get<SettingService>();
            _soundSlider.value = _settingService.SettingsInfo.SoundVolume;
            _soundSlider.onValueChanged.AddListener(UpdateSoundVolume);
            InitializeInterfaceLanguage();
        }

        private void UpdateSoundVolume(float volume)
        {
            _settingService.ChangeSound(volume);
        }

        private void InitializeInterfaceLanguage()
        {
            _interfaceLanguageDropdown.ClearOptions();
            var interfaceLocalizations = _settingService.InterfaceLocales;
            List<string> interfaceOptions = new List<string>();
            foreach (var l in interfaceLocalizations)
            {
                interfaceOptions.Add($"{l.LocaleName}");
            }

            _interfaceLanguageDropdown.AddOptions(interfaceOptions);
            var savedLocaleCode = _settingService.SettingsInfo.InterfaceLanguage;
            var savedIndex = interfaceLocalizations.FindIndex(i => i.Identifier == savedLocaleCode);
            _interfaceLanguageDropdown.SetValueWithoutNotify(savedIndex);
            _interfaceLanguageDropdown.onValueChanged.AddListener(ChangeInterfaceLanguage);
        }

        private void ChangeInterfaceLanguage(int index)
        {
            _interfaceLanguageDropdown.interactable = false;
            var selected = _settingService.InterfaceLocales[index];
            bool isSame = LocalizationSettings.Instance.GetSelectedLocale().Identifier == selected.Identifier;
            if (isSame)
            {
                return;
            }

            _settingService.ChangeInterfaceLanguage(selected);
            _interfaceLanguageDropdown.interactable = true;
        }
    }
}