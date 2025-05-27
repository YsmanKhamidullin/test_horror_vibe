using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Game.VisualNovel.Core.UI
{
    public class ButtonDialogue : Dialogue
    {
        private bool isPassed;

        [SerializeField]
        private List<Button> _buttons;

        public override async UniTask<string> Show()
        {
     
            UpdateAll();
            gameObject.SetActive(true);
            foreach (var b in _buttons)
            {
                b.onClick.AddListener(MarkIsPassed);
            }


            await UniTask.WaitUntil(() => isPassed);
            await UniTask.WaitForSeconds(0.2f);
            return "";
        }

        private void MarkIsPassed()
        {
            foreach (var b in _buttons)
            {
                b.onClick.RemoveAllListeners();
            }

            isPassed = true;
        }
    }
}