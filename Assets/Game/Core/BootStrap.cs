using Cysharp.Threading.Tasks;
using Game.Core.Architecture;
using Game.Core.Architecture.Services;
using UnityEngine;

namespace Game.Core
{
    public class BootStrap : MonoBehaviour
    {
        private void Awake()
        {
            Boot().Forget();
            Destroy(gameObject);
        }

        private async UniTask Boot()
        {
            await Project.Initialize();
            await UniTask.WaitForEndOfFrame();
            await UniTask.WaitForEndOfFrame();
            SceneService.LoadMenuScene();
        }
    }
}