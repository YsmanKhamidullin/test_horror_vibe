using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Core.Canvas
{
    public class CanvasWindow : BaseWindow
    {
        public bool _inCoversation;

        [SerializeField]
        private GraphicRaycaster _graphicRaycaster;

        [SerializeField]
        private TMP_Text _controlsText;

        [SerializeField]
        private Image _subBackgroundShadow;

        [SerializeField]
        private TMP_Text _subText;

        private string _storedControlsText;
        private Coroutine _subTexRoutine;
        private float _subDuration;

        public void DisableRaycast()
        {
            _graphicRaycaster.enabled = false;
        }

        public void EnableRaycast()
        {
            _graphicRaycaster.enabled = true;
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

        private IEnumerator DeactivateSubAfterTime()
        {
            yield return new WaitForSeconds(_subDuration);
            _subTexRoutine = null;
            Image image = _subBackgroundShadow;
            TMP_Text tMpText = _subText;
            tMpText.enabled = false;
            image.enabled = false;
            yield return null;
        }

        public void ShowSubText(string text, float forTime = 5f)
        {
            _subDuration = forTime;
            _subText.text = text;
            Image image = _subBackgroundShadow;
            bool flag2 = (_subText.enabled = true);
            image.enabled = flag2;
            if (_subTexRoutine == null)
            {
                StartCoroutine(DeactivateSubAfterTime());
                return;
            }

            StopCoroutine(_subTexRoutine);
            _subTexRoutine = null;
            StartCoroutine(DeactivateSubAfterTime());
        }
    }
}