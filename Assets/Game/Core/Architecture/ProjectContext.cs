using Game.Core.Architecture.Services;
using Game.Core.Player;
using Game.Core.Utils;
using Game.VisualNovel.Core;
using UnityEngine;

namespace Game.Core.Architecture
{
    public class ProjectContext : MonoBehaviour
    {
        public DialogueSequenceWrapper DialogueSequenceWrapper
        {
            get
            {
                if (_dialogueSequenceWrapper == null)
                {
                    _dialogueSequenceWrapper = gameObject.AddComponent<DialogueSequenceWrapper>();
                }

                return _dialogueSequenceWrapper;
            }
        }
        public AudioService AudioService
        {
            get
            {
                if (_audioService == null)
                {
                    SetAudioService();
                }

                return _audioService;
            }
        }

        public PlayerController PlayerController
        {
            get
            {
                if (_playerController == null)
                {
                    SetPlayer();
                }

                return _playerController;
            }
        }

        public static bool IsCreated { get; private set; }

        private AudioService _audioService;
        private PlayerController _playerController;
        private DialogueSequenceWrapper _dialogueSequenceWrapper;

        public static ProjectContext Create()
        {
            var project = new GameObject("Project");
            var projectContext = project.AddComponent<ProjectContext>();
            DontDestroyOnLoad(project);
            IsCreated = true;
            return projectContext;
        }

        public void Initialize()
        {
            SetPlayer();
            SetAudioService();
            CreateDebugger();
        }

        private void CreateDebugger()
        {
#if UNITY_EDITOR
            gameObject.AddComponent<DebugHotKeysListener>();
#endif
        }

        private void SetPlayer()
        {
            if (_playerController == null)
            {
                _playerController = FindFirstObjectByType<PlayerController>(FindObjectsInactive.Exclude);
            }
        }

        private void SetAudioService()
        {
            if (_audioService == null)
            {
                _audioService = FindFirstObjectByType<AudioService>(FindObjectsInactive.Exclude);
            }
        }
    }
}