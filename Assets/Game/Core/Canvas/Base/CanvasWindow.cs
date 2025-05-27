using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Game.Core.Canvas.Base
{
    public class CanvasWindow : BaseWindow
    {
        public bool _inCoversation;

        [field: SerializeField]
        public Transform VisualNovelParent { get; private set; }

        [field: SerializeField]
        public MoneyUi MoneyUi { get; private set; }

        [SerializeField]
        private GraphicRaycaster _graphicRaycaster;

        [SerializeField]
        private TMP_Text _controlsText;

        [SerializeField]
        private Image _subBackgroundShadow;

        [SerializeField]
        private TMP_Text _subText;

        [SerializeField]
        private TrueEnding _trueEnding;

        private string _storedControlsText;
        private CancellationTokenSource _subTextCts;


        protected void OnDestroy()
        {
            _subTextCts?.Cancel();
            _subTextCts?.Dispose();
        }


        public void DisableRaycast()
        {
            _graphicRaycaster.enabled = false;
        }

        public void EnableRaycast()
        {
            _graphicRaycaster.enabled = true;
        }

        public void ShowFinalText()
        {
            _trueEnding.Animate().Forget();
        }

        public void ClearControlsText()
        {
            _controlsText.text = "";
            LoadStoredControlsText();
        }

        public void LoadStoredControlsText()
        {
            _controlsText.text = _storedControlsText;
        }

        public void ShowControlsText(string text)
        {
            _controlsText.text = text;
        }

        private async UniTask DeactivateSubAfterTimeAsync(float duration, CancellationToken cancellationToken)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(duration), cancellationToken: cancellationToken);
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            _subText.enabled = false;
            _subBackgroundShadow.enabled = false;
        }

        public void ShowSubText(string text, float forTime = -1f)
        {
            if (Mathf.Approximately(forTime, -1f))
            {
                forTime = text.Length / 10f;
            }

            Debug.Log($"Text: {text}");

            _subTextCts?.Cancel();
            _subTextCts?.Dispose();
            _subTextCts = new CancellationTokenSource();

            _subText.text = text;
            _subText.enabled = true;
            _subBackgroundShadow.enabled = true;

            DeactivateSubAfterTimeAsync(forTime, _subTextCts.Token).Forget();
        }
    }
}