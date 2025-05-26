using System;
using Game.Core.Architecture;
using Game.Core.Architecture.Services;
using Game.Core.Canvas;
using Game.Core.InteractItems;
using Game.Localization.Scripts;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Core.Player
{
    public class PlayerController : MonoBehaviour
    {
        public event Action OnHoldObject;
        public event Action OnThrowObject;

        [SerializeField]
        private interactableObjectUI _interactObject;

        [field: SerializeField]
        public bool IsPlayerPaused { get; private set; }

        [SerializeField]
        private Transform _handPosition;

        [SerializeField]
        protected Holdable _currentHoldingObject;

        [SerializeField]
        private Transform _cameraTransform;

        protected Iinteractable _objectToInteract;
        private InputService _inputService;
        private CanvasWindow _canvasWindow;

        protected async void Start()
        {
            _inputService = await Project.Get<InputService>();
            var a = await Project.Get<GameWindowsService>();
            _canvasWindow = a.Get<CanvasWindow>();
            
            _interactObject.FoundInteractObject += FoundInteractObject;
            _inputService.OnInteract += Interact;
            _inputService.OnThrow += ThrowItem;
            _inputService.OnZoom += OnZoom;
        }

        private void OnDestroy()
        {
            _interactObject.FoundInteractObject -= FoundInteractObject;
            _inputService.OnInteract -= Interact;
            _inputService.OnThrow -= ThrowItem;
            _inputService.OnZoom -= OnZoom;
        }

        private void FoundInteractObject(Iinteractable interactable)
        {
            _objectToInteract = interactable;
        }

        private void OnZoom()
        {
        }

        private void ThrowItem()
        {
            if (_currentHoldingObject == null)
            {
                return;
            }

            _currentHoldingObject.Throw(_cameraTransform);
            _currentHoldingObject = null;
            _canvasWindow.ClearControlsText();
            this.OnThrowObject?.Invoke();
        }

        private void Interact()
        {
            if (!_canvasWindow._inCoversation && _objectToInteract != null)
            {
                _objectToInteract.Clicked(delegate { _currentHoldingObject = null; });
            }
        }

        public virtual void HoldObject(Holdable holdable, bool isThrowable = false)
        {
            if (_currentHoldingObject == null)
            {
                _currentHoldingObject = holdable;

                holdable.GoToPosition(_handPosition);
                if (isThrowable)
                {
                    Debug.Log("Throw Controls Activated");
                    _canvasWindow.ShowControlsText(LocalizationWrapper.Get("Throw"));
                }

                this.OnHoldObject?.Invoke();
            }
        }

        public Holdable GetHoldingObject()
        {
            return _currentHoldingObject;
        }

        public void RemoveHandObject()
        {
            _canvasWindow.ClearControlsText();
            _currentHoldingObject = null;
        }
    }
}