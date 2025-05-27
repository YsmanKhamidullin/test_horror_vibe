using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Splines;
using System;
using System.Threading;
using Game.Core.Architecture;

namespace Game.Core.Npc
{
    public class Car : MonoBehaviour
    {
        [SerializeField]
        private SplineAnimate _splineAnimate;

        [SerializeField]
        private GameObject _blueLight;
        
        [SerializeField]
        private GameObject _redLight;

        [SerializeField]
        private float _lightFlashInterval = 0.3f;

        private bool _isLightsActive;
        private CancellationTokenSource _lightsCts;

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        public async UniTask MoveCarToEntrance()
        {
            gameObject.SetActive(true);
            Project.ProjectContext.AudioService.PoliceSiren.Play();
            StartPoliceLights();
            _splineAnimate.Play();
            await UniTask.WaitUntil(() => _splineAnimate.NormalizedTime >= 0.99f);
        }

        private void StartPoliceLights()
        {
            _isLightsActive = true;
            _lightsCts = new CancellationTokenSource();
            FlashLightsAsync().Forget();
        }

        private async UniTask FlashLightsAsync()
        {
            try
            {
                while (_isLightsActive)
                {
                    _blueLight.SetActive(true);
                    _redLight.SetActive(false);
                    await UniTask.Delay(TimeSpan.FromSeconds(_lightFlashInterval), cancellationToken: _lightsCts.Token);
                    
                    _blueLight.SetActive(false);
                    _redLight.SetActive(true);
                    await UniTask.Delay(TimeSpan.FromSeconds(_lightFlashInterval), cancellationToken: _lightsCts.Token);
                }
            }
            catch (OperationCanceledException)
            {
                // Normal cancellation, do nothing
            }
        }

        private void OnDestroy()
        {
            _isLightsActive = false;
            _lightsCts?.Cancel();
            _lightsCts?.Dispose();
        }
    }
}