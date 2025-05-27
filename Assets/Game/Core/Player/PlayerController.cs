using System;
using Game.Core.Architecture;
using Game.Core.Architecture.Services;
using Game.Core.Canvas;
using Game.Core.Canvas.Base;
using Game.Core.InteractItems;
using Game.Localization.Scripts;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Core.Player
{
    public class PlayerController : MonoBehaviour
    {
        public event Action OnHoldObject;
        public event Action OnPutObject;
        public event Action OnThrowObject;

        [field: SerializeField]
        public bool IsPlayerPaused { get; private set; }

        [field: SerializeField]
        public CanvasWindow CanvasWindow { get; private set; }

        [SerializeField]
        private interactableObjectUI _interactObject;

        [SerializeField]
        private Transform _handPosition;

        [SerializeField]
        protected Holdable _currentHoldingObject;

        [SerializeField]
        private Transform _cameraTransform;

        protected Iinteractable _objectToInteract;
        private InputService _inputService;

        protected async void Start()
        {
            _inputService = await Project.Get<InputService>();
            _interactObject.FoundInteractObject += FoundInteractObject;
            _inputService.OnInteract += Interact;
            _inputService.OnPut += PutItem;
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

        private void PutItem()
        {
            if (_currentHoldingObject == null)
            {
                return;
            }

            _currentHoldingObject.Throw(_cameraTransform, 50);
            _currentHoldingObject = null;
            CanvasWindow.ClearControlsText();
            this.OnPutObject?.Invoke();
        }

        private void ThrowItem()
        {
            if (_currentHoldingObject == null)
            {
                return;
            }

            _currentHoldingObject.Throw(_cameraTransform);
            _currentHoldingObject = null;
            CanvasWindow.ClearControlsText();
            this.OnThrowObject?.Invoke();
        }

        private void Interact()
        {
            if (!CanvasWindow._inCoversation && _objectToInteract != null)
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
                    var text = LocalizationWrapper.Get("Put") + "[F]" + "\n" +
                               LocalizationWrapper.Get("Throw") + "[G]";
                    CanvasWindow.ShowControlsText(text);
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
            CanvasWindow.ClearControlsText();
            _currentHoldingObject = null;
        }
    }
}