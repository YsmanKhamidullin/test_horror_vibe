using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Core.Architecture.Services.Base;
using UnityEngine.SceneManagement;

namespace Game.Core.Architecture.Services
{
    public class SceneService : BaseService
    {
        public static List<string> AllScenes;

        public override UniTask Initialize()
        {
            AllScenes = new List<string>();
            for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
            {
                var s = SceneManager.GetSceneByBuildIndex(i);
                AllScenes.Add(s.name);
            }

            return UniTask.CompletedTask;
        }

        public override void PostInitialize()
        {
        }

        public static void LoadBoot()
        {
            LoadScene("Boot");
        }

        public static void LoadGameScene()
        {
            LoadScene("Game");
        }

        public static void LoadScene(string name)
        {
            SceneManager.LoadScene(name);
        }
    }
}