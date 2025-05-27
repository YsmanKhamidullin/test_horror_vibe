using System;
using DG.Tweening;
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
            _image.DOFade(1f, 0.5f).From(0f).SetEase(Ease.OutSine);
            
            _image.transform.DOMove(_targetMovePos.position, 0.75f).From(_startMovePos.position).SetEase(Ease.OutQuad);
        }
    }
}