using Cysharp.Threading.Tasks;
using Game.Core.Architecture.Services.Base;
using UnityEngine;

namespace Game.Core.Architecture.Services
{
    public class InputService : BaseService
    {
        public delegate void InputControl();

        public event InputControl OnInteract;

        public event InputControl OnPut;
        
        public event InputControl OnThrow;

        public event InputControl OnZoom;

        public event InputControl OnAnyKey;

        public event InputControl OnGetUp;

        public event InputControl OnEsc;

        public event InputControl OnFlashlight;

        private InputBehaviour _behaviour;

        public override UniTask Initialize()
        {
            return UniTask.CompletedTask;
        }

        public override void OnProjectContextCreated(ProjectContext p)
        {
            base.OnProjectContextCreated(p);
            _behaviour = p.gameObject.AddComponent<InputBehaviour>();
            _behaviour.Initialize(this);
        }

        private class InputBehaviour : MonoBehaviour
        {
            private InputService _inputService;

            public void Initialize(InputService inputService)
            {
                _inputService = inputService;
            }

            private void Update()
            {
                if (Input.GetMouseButtonDown(0) && _inputService.OnInteract != null)
                {
                    _inputService.OnInteract();
                }

                if (Input.GetKeyDown(KeyCode.G) && _inputService.OnThrow != null)
                {
                    _inputService.OnThrow();
                }

                if (Input.GetKeyDown(KeyCode.F) && _inputService.OnPut != null)
                {
                    _inputService.OnPut();
                }

                if (Input.GetMouseButtonDown(1) && _inputService.OnZoom != null)
                {
                    _inputService.OnZoom();
                }

                if (Input.GetKeyDown(KeyCode.Space) && _inputService.OnGetUp != null)
                {
                    _inputService.OnGetUp();
                }

                if (Input.GetKeyDown(KeyCode.Escape) && _inputService.OnEsc != null)
                {
                    _inputService.OnEsc();
                }

                if (Input.anyKeyDown && _inputService.OnAnyKey != null)
                {
                    _inputService.OnAnyKey();
                }

                if (Input.GetKeyDown(KeyCode.F) && _inputService.OnFlashlight != null)
                {
                    _inputService.OnFlashlight();
                }
            }
        }
    }
}