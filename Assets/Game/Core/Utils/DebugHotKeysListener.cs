using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.Core.Architecture;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Game.Core.Utils
{
    public class DebugHotKeysListener : MonoBehaviour
    {
#if UNITY_EDITOR

        // private bool _isCursorVisible;

        private void Start()
        {
            // _isCursorVisible = false;
            // Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
#endif

        private void Update()
        {
#if UNITY_EDITOR

            if (Input.GetKey(KeyCode.Equals))
            {
                Time.timeScale = 5;
            }
            else if (Input.GetKey(KeyCode.Minus))
            {
                Time.timeScale = 0.25f;
            }
            else
            {
                Time.timeScale = 1;
            }

            if (Input.GetKeyDown(KeyCode.F8))
            {
                AddCurrency().Forget();
            }

            if (Input.GetKeyDown(KeyCode.F5))
            {
                ReloadScene().Forget();
            }
#endif
        }

        private async UniTask AddCurrency()
        {
            
        }

        private async UniTask ReloadScene()
        {
            DOTween.KillAll();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}