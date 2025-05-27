using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.Core.Utils;
using Game.VisualNovel.Core.Tools;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Core.Canvas.Base
{
    public class TrueEnding : BaseWindow
    {
        [SerializeField]
        private SlowRevealText _label;

        public async UniTask Animate()
        {
            _canvasGroup.alpha = 0f;
            _label.SetText("Game by: TsukiGan");
            await _canvasGroup.DOFade(1f, 1.5f).SetEase(Ease.OutSine).ToUniTask();
            await UniTask.WaitForSeconds(0.2f);
            await _label.Show();
            await UniTask.WaitForSeconds(2f);

            _label.SetText("Thank you for playing!");
            await _label.Show();
            await UniTask.WaitForSeconds(3f);
        }
    }
}