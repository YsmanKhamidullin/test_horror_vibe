using Game.Core.Utils;
using UnityEngine;

namespace Game.Core.Architecture
{
    public class ProjectContext : MonoBehaviour
    {
        public static bool IsCreated { get; private set; }

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
            CreatePlayer();
            CreateDebugger();
        }

        private void CreateDebugger()
        {
#if UNITY_EDITOR
            gameObject.AddComponent<DebugHotKeysListener>();
#endif
        }

        private void CreatePlayer()
        {
        }
    }
}