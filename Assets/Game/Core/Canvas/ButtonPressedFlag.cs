using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Core.Canvas
{
    [RequireComponent(typeof(Button))]
    public class ButtonPressedFlag : MonoBehaviour
    {
        [field: SerializeField]
        public Button Button { get; private set; }
        
        [field: SerializeField]
        public bool IsPressed { get; private set; }

        private void Awake()
        {
            Button.onClick.AddListener(SetPressed);
        }

        private void OnDestroy()
        {
            Button.onClick.RemoveListener(SetPressed);
        }

        public void SetPressed()
        {
            IsPressed = true;
            SetNotPressedNextFrame().Forget();
        }

        private async UniTask SetNotPressedNextFrame()
        {
            await UniTask.WaitForEndOfFrame();
            await UniTask.WaitForEndOfFrame();
            IsPressed = false;
        }
    }
}