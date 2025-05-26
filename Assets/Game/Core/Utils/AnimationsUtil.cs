using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Core.Utils
{
    public static class AnimationsUtil
    {
        public static async UniTask ToOneAlpha(CanvasGroup canvasGroup, Ease ease = Ease.InOutSine)
        {
            await canvasGroup.DOFade(1f, 0.8f).SetEase(ease).ToUniTask();
        }

        public static async UniTask ToOneAlpha(Image image, float time)
        {
            await image.DOFade(1f, time).ToUniTask();
        }

        public static async UniTask ToZeroAlpha(Image image, float time)
        {
            await image.DOFade(0f, time).ToUniTask();
        }

        public static async UniTask ToZeroAlpha(CanvasGroup canvasGroup, float time = 1.2f, Ease ease = Ease.InOutSine)
        {
            await canvasGroup.DOFade(0f, time).SetEase(ease).ToUniTask();
        }

        public static async UniTask MoveToFrom(Transform transform, Vector3 target, Vector3 from, float time = 1.4f,
            Ease ease = Ease.InOutSine)
        {
            transform.position = from;
            await transform.DOMove(target, time).SetEase(ease).ToUniTask();
        }

        public static async UniTask MoveAnchoredToFrom(RectTransform transform, Vector2 target, Vector2 from,
            float time = 1.4f,
            Ease ease = Ease.InOutSine)
        {
            transform.anchoredPosition = from;
            await transform.DOAnchorPos(target, time).SetEase(ease).ToUniTask();
        }

        public static async UniTask MoveAnchored(this RectTransform transform, Vector2 pos, float time = 1f,
            Ease ease = Ease.InOutSine)
        {
            await transform.DOAnchorPos(pos, time).SetEase(ease).ToUniTask();
        }

        public static async UniTask ScaleToFrom(Transform transform, float target, float from, float time = 1.4f,
            Ease ease = Ease.InOutSine)
        {
            transform.localScale = Vector3.one * from;
            await transform.DOScale(target, time).SetEase(ease).ToUniTask();
        }

        public static async UniTask Fill(Image image, float fillAmount, float time, Ease ease = Ease.OutSine)
        {
            await image.DOFillAmount(fillAmount, time).SetEase(ease).ToUniTask();
        }

        public static async UniTask Alpha(Image image, float fillAmount, float time, Ease ease = Ease.OutSine)
        {
            await image.DOFade(fillAmount, time).SetEase(ease).ToUniTask();
        }
    }
}