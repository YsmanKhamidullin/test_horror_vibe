using System;
using DG.Tweening;
using Game.Core.Architecture;
using Game.Core.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Core.Canvas
{
    public class MoneyUi : MonoBehaviour
    {
        [SerializeField]
        private Image _image;

        [SerializeField]
        private Transform _startMovePos;

        [SerializeField]
        private Transform _targetMovePos;

        private void Awake()
        {
            _image.Alpha(0f);
        }

        public void Show()
        {
            Project.ProjectContext.AudioService.Money.Play();
            
            _image.DOFade(1f, 0.5f).From(0f).SetEase(Ease.OutSine);
            
            _image.transform.DOMove(_targetMovePos.position, 0.75f).From(_startMovePos.position).SetEase(Ease.OutQuad);

            DOTween.Sequence().InsertCallback(2f, () =>
            {
                _image.DOFade(0f, 0.25f).From(0f).SetEase(Ease.InSine);
            });
        }
    }
}