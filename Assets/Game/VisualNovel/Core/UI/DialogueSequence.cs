using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Core.Architecture.Services;
using Game.Localization.Scripts;
using Game.VisualNovel.Scripts.Attributes.IntSlider;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

namespace Game.VisualNovel.Core.UI
{
    public class DialogueSequence : MonoBehaviour
    {
        [SerializeField]
        private Button _skipButton;

        [IntSlider(0, 15)]
        [SerializeField]
        private int _currentIndex;

        [SerializeField]
        private Transform _dialoguesParent;

        [SerializeField]
        private List<Dialogue> _dialogues;

        [SerializeField]
        private DialogueSequenceCharacters _dialogueSequenceCharacters;

        [SerializeField]
        private CanvasGroup _canvasGroup;

        private CancellationTokenSource _cancellationToken;

        private void Awake()
        {
            _skipButton.onClick.AddListener(SkipSequence);
        }

        public async UniTask Play()
        {
            HideAll();
            _skipButton.gameObject.SetActive(false);
#if UNITY_EDITOR
            _skipButton.gameObject.SetActive(true);
#endif
            _currentIndex = 0;

            _cancellationToken = new CancellationTokenSource();
            await Next().AttachExternalCancellation(_cancellationToken.Token).SuppressCancellationThrow();
        }

        public async UniTask Next()
        {
            if (_currentIndex >= _dialogues.Count)
            {
                return;
            }

            var dialogue = _dialogues[_currentIndex];
            _dialogueSequenceCharacters.SetUp(dialogue);
            var dialogueText = await dialogue.Show();
            if (dialogue is not OnBoardingDialogue)
            {
                if (dialogue is not ButtonDialogue)
                {
                    await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0));
                }
            }

            dialogue.Hide();
            _currentIndex += 1;
            await Next();
        }

        private void SkipSequence()
        {
            _cancellationToken?.Cancel();
        }

        public void HideAll()
        {
            _skipButton.gameObject.SetActive(false);
            foreach (var dialogue in _dialogues)
            {
                dialogue.Hide();
            }
        }

        public void ZeroAlpha()
        {
            _canvasGroup.alpha = 0f;
            _canvasGroup.interactable = false;
        }

        public void OneAlpha()
        {
            _canvasGroup.alpha = 1f;
            _canvasGroup.interactable = true;
        }

#if UNITY_EDITOR

        #region EditorTools

        private void OnValidate()
        {
            if (Application.isPlaying)
                return;
            UpdateAndLocalize();
            UpdateEditor();
        }

        private void OnDrawGizmos()
        {
            UpdateAndLocalize();
            UpdateEditor();
        }

        private void UpdateEditor()
        {
            if (_dialogues.Count == 0)
            {
                return;
            }

            var maxV = _dialogues.Count - 1;
            _currentIndex = Mathf.Clamp(_currentIndex, 0, maxV);
            foreach (var dialogue in _dialogues)
            {
                dialogue.gameObject.SetActive(false);
            }

            _dialogues[_currentIndex].gameObject.SetActive(true);
        }

        [Button]
        private void UpdateAndLocalize()
        {
            _dialogues = new List<Dialogue>(_dialoguesParent.GetComponentsInChildren<Dialogue>(true));
            string rootName = gameObject.name;
            for (int i = 0; i < _dialogues.Count; i++)
            {
                string lK = rootName + $"_text_{i}";
                _dialogues[i].Text = LocalizationWrapper.Get(lK);
                _dialogues[i].UpdateAll();
            }
        }

        [Space(10)]
        [Header("Editor Tools")]
        [SerializeField]
        [MinMaxSlider(0, 1f)]
        private Vector2 _alphaMinMax;

        [SerializeField]
        private float _alphaStep;

        [Button]
        private void SetBackgroundAlphaFromCurrentToLast()
        {
            var curCharacters = _dialogues[_currentIndex].VisibleCharacters;
            int j = 0;
            for (int i = _currentIndex; i < _dialogues.Count; i++)
            {
                j++;
                var currentAlpha = _alphaMinMax.x + Mathf.Clamp(_alphaStep * j, _alphaMinMax.x, _alphaMinMax.y);
                _dialogues[i].SetBackgroundAlpha(currentAlpha);
            }
        }

        #endregion

#endif
    }
}