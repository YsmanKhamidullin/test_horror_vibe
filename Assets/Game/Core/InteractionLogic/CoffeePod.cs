using System;
using Game.Core.InteractItems;
using Game.Localization.Scripts;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Core.InteractionLogic
{
    public class CoffeePod : Holdable, Iinteractable
    {
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
                playerController.HoldObject(this);
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
    }
}