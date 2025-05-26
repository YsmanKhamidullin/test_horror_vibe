using Game.Core.Player;
using Game.Core.Utils;
using UnityEngine;

namespace Game.Core.Architecture
{
    public class ProjectContext : MonoBehaviour
    {
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

        private PlayerController _playerController;

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
            _playerController = FindFirstObjectByType<PlayerController>(FindObjectsInactive.Exclude);
        }
    }
}