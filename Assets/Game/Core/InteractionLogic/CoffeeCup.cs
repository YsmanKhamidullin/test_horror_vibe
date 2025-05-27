using System;
using Game.Core.InteractItems;
using Game.Localization.Scripts;
using UnityEngine;

namespace Game.Core.InteractionLogic
{
    public class CoffeeCup : Holdable, Iinteractable
    {
        public Animator CoffeeAnimator;
        
        public bool IsFilled { get; private set; }

        public bool IsCapEquiped { get; private set; }

        [SerializeField]
        private GameObject _cap;

        [SerializeField]
        private Vector3 _holdPos;

        [SerializeField]
        private Vector3 _holdRot;

        public void Clicked(Action removedFromHand)
        {
            if (playerController.GetHoldingObject() != null)
            {
                _canvasWindow.ShowSubText(LocalizationWrapper.Get("HandsFull"));
            }
            else
            {
                playerController.HoldObject(this, true);
            }
        }

        public override void SetForUse()
        {
            base.SetForUse();
            GetComponent<Collider>().enabled = false;
        }

        public override void GoToPosition(Transform parentTransform)
        {
            base.GoToPosition(parentTransform);
            base.transform.localPosition = _holdPos;
            base.transform.localRotation = Quaternion.Euler(_holdRot);
        }


        public void FilledWithCoffee()
        {
            IsFilled = true;
        }

        public void EnableCap()
        {
            CoffeeAnimator.gameObject.SetActive(false);
            _cap.SetActive(true);
            IsCapEquiped = true;
        }
    }
}