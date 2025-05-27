using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Game.VisualNovel.Core.UI
{
    public class OnBoardingDialogue : Dialogue
    {
        [SerializeField]
        private GameObject OnboardingParent;

        [SerializeField]
        private Button OnboardingButton;

        private bool isPassed;

        public override async UniTask<string> Show()
        {
            var r = await base.Show();
            OnboardingParent.SetActive(true);
            OnboardingButton.onClick.AddListener(MarkOnboardingPassed);
            await UniTask.WaitUntil(() => isPassed);
            return r;
        }

        private void MarkOnboardingPassed()
        {
            OnboardingParent.SetActive(false);
            isPassed = true;
        }
    }
}