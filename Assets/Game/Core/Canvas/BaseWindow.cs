using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.Core.Architecture;
using Game.Core.Architecture.Services;
using Game.Core.Utils;
using UnityEngine;

namespace Game.Core.Canvas
{
    public abstract class BaseWindow : MonoBehaviour
    {
        [SerializeField]
        protected CanvasGroup _canvasGroup;

        [SerializeField]
        private RectTransform _windowContent;

        protected WindowAnimations _windowAnimations;

        protected virtual async void Awake()
        {
            _windowAnimations = new WindowAnimations();
            _windowAnimations.Initialize(_windowContent);
            Project.Get<GameWindowsService>().Register(this);
        }

        public void HideByCanvasGroup()
        {
            _canvasGroup.alpha = 0f;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }

        public void ShowByCanvasGroup()
        {
            _canvasGroup.alpha = 1f;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }

        public async UniTask HideByCanvasGroupAsync()
        {
            await _canvasGroup.DOFade(0f, 0.75f).SetEase(Ease.OutSine).ToUniTask();
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }

        public async UniTask ShowByCanvasGroupAsync()
        {
            await _canvasGroup.DOFade(1f, 0.75f).SetEase(Ease.InSine).ToUniTask();
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }

        public async UniTask ShowUntilHide(Action onShow = null)
        {
            await Show();
            onShow?.Invoke();
            await UniTask.WaitUntil(() => !gameObject.activeSelf, cancellationToken: destroyCancellationToken);
        }

        public async UniTask Show()
        {
            Project.Get<GameWindowsService>().Get<CanvasWindow>().DisableRaycast();
            await OnBeforeShow();
            gameObject.SetActive(true);
            await OnAfterShow();
            Project.Get<GameWindowsService>().Get<CanvasWindow>().EnableRaycast();
        }

        protected virtual async UniTask OnBeforeShow()
        {
        }

        protected virtual async UniTask OnAfterShow()
        {
        }

        public virtual async UniTask Hide()
        {
            Project.Get<GameWindowsService>().Get<CanvasWindow>().DisableRaycast();
            await OnBeforeHide();
            gameObject.SetActive(false);
            await OnAfterHide();
            Project.Get<GameWindowsService>().Get<CanvasWindow>().EnableRaycast();
        }

        protected virtual async UniTask OnBeforeHide()
        {
        }

        protected virtual async UniTask OnAfterHide()
        {
        }
    }
}