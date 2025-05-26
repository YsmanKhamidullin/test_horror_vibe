using Cysharp.Threading.Tasks;
using Game.Core.Utils;
using UnityEngine;

namespace Game.Core.Canvas
{
    public class WindowAnimations
    {
        private RectTransform _windowContent;

        public void Initialize(RectTransform windowContent)
        {
            _windowContent = windowContent;
        }
        
        public async UniTask ToOutsideLeft(bool isMomentum = false)
        {
            SetAnchorToLeft(_windowContent);
            var pos = new Vector2(-_windowContent.rect.width / 2f, 0);
            if (isMomentum)
            {
                _windowContent.anchoredPosition = pos;
            }
            else
            {
                await _windowContent.MoveAnchored(pos, 0.5f);
            }
        }

        public async UniTask ToInsideLeft(bool isMomentum = false)
        {
            SetAnchorToLeft(_windowContent);
            var pos = new Vector2(_windowContent.rect.width / 2f, 0);
            if (isMomentum)
            {
                _windowContent.anchoredPosition = pos;
            }
            else
            {
                await _windowContent.MoveAnchored(pos, 0.5f);
            }
        }

        public async UniTask ToOutsideRight(bool isMomentum = false)
        {
            SetAnchorToRight(_windowContent);
            var pos = new Vector2(_windowContent.rect.width / 2f, 0);
            if (isMomentum)
            {
                _windowContent.anchoredPosition = pos;
            }
            else
            {
                await _windowContent.MoveAnchored(pos, 0.5f);
            }
        }

        public async UniTask ToInsideRight(bool isMomentum = false)
        {
            SetAnchorToRight(_windowContent);
            var pos = new Vector2(-_windowContent.rect.width / 2f, 0);
            if (isMomentum)
            {
                _windowContent.anchoredPosition = pos;
            }
            else
            {
                await _windowContent.MoveAnchored(pos, 0.5f);
            }
        }

        public async UniTask ToOutsideTop(bool isMomentum = false)
        {
            SetAnchorToTop(_windowContent);
            var pos = new Vector2(0, _windowContent.rect.height / 2f);
            if (isMomentum)
            {
                _windowContent.anchoredPosition = pos;
            }
            else
            {
                await _windowContent.MoveAnchored(pos, 0.5f);
            }
        }

        public async UniTask ToInsideTop(bool isMomentum = false)
        {
            SetAnchorToTop(_windowContent);
            var pos = new Vector2(0, -_windowContent.rect.height / 2f);
            if (isMomentum)
            {
                _windowContent.anchoredPosition = pos;
            }
            else
            {
                await _windowContent.MoveAnchored(pos, 0.5f);
            }
        }

        public async UniTask ToOutsideBottom(bool isMomentum = false)
        {
            SetAnchorToDown(_windowContent);
            var pos = new Vector2(0, -_windowContent.rect.height / 2f);
            if (isMomentum)
            {
                _windowContent.anchoredPosition = pos;
            }
            else
            {
                await _windowContent.MoveAnchored(pos, 0.5f);
            }
        }

        public async UniTask ToInsideBottom(bool isMomentum = false)
        {
            SetAnchorToDown(_windowContent);
            var pos = new Vector2(0, _windowContent.rect.height / 2f);
            if (isMomentum)
            {
                _windowContent.anchoredPosition = pos;
            }
            else
            {
                await _windowContent.MoveAnchored(pos, 0.5f);
            }
        }

        public async UniTask ToCenter(bool isMomentum = false)
        {
            SetAnchorToCenter(_windowContent);
            var pos = new Vector2(0, 0);
            if (isMomentum)
            {
                _windowContent.anchoredPosition = pos;
            }
            else
            {
                await _windowContent.MoveAnchored(pos, 0.5f);
            }
        }

        public async UniTask ToFullScreenCenter(bool isMomentum = false)
        {
            SetAnchorToFullScreenCenter(_windowContent);
            var pos = new Vector2(0, 0);
            if (isMomentum)
            {
                _windowContent.anchoredPosition = pos;
            }
            else
            {
                await _windowContent.MoveAnchored(pos, 0.5f);
            }
        }

        public static void SetAnchorToFullScreenCenter(RectTransform target)
        {
            var originalPosition = target.localPosition;
            target.anchorMax = new Vector2(1f, 1f);
            target.anchorMin = new Vector2(0, 0);
            target.sizeDelta = new Vector2(0, 0);
            target.localPosition = originalPosition;
        }


        public static void SetAnchorToCenter(RectTransform target)
        {
            var originalSize = target.rect;
            var originalPosition = target.localPosition;
            target.anchorMax = new Vector2(0.5f, 0.5f);
            target.anchorMin = new Vector2(0.5f, 0.5f);
            target.localPosition = originalPosition;
            target.sizeDelta = new Vector2(originalSize.width, originalSize.height);
        }

        public static void SetAnchorToLeft(RectTransform target)
        {
            var originalSize = target.rect;
            var originalPosition = target.localPosition;
            target.anchorMin = new Vector2(0.0f, 0.5f);
            target.anchorMax = new Vector2(0.0f, 0.5f);
            target.localPosition = originalPosition;
            target.sizeDelta = new Vector2(originalSize.width, originalSize.height);
        }

        public static void SetAnchorToRight(RectTransform target)
        {
            var originalSize = target.rect;
            var originalPosition = target.localPosition;
            target.anchorMax = new Vector2(1f, 0.5f);
            target.anchorMin = new Vector2(1f, 0.5f);
            target.localPosition = originalPosition;
            target.sizeDelta = new Vector2(originalSize.width, originalSize.height);
        }

        public static void SetAnchorToTop(RectTransform target)
        {
            var originalSize = target.rect;
            var originalPosition = target.localPosition;
            target.anchorMax = new Vector2(0.5f, 1f);
            target.anchorMin = new Vector2(0.5f, 1f);
            target.localPosition = originalPosition;
            target.sizeDelta = new Vector2(originalSize.width, originalSize.height);
        }


        public static void SetAnchorToDown(RectTransform target)
        {
            var originalSize = target.rect;
            var originalPosition = target.localPosition;
            target.anchorMax = new Vector2(0.5f, 0f);
            target.anchorMin = new Vector2(0.5f, 0f);
            target.localPosition = originalPosition;
            target.sizeDelta = new Vector2(originalSize.width, originalSize.height);
        }
    }
}