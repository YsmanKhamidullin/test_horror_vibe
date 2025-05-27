using System;
using Game.Core.InteractItems;
using Game.Core.Npc;
using Game.Localization.Scripts;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Core.InteractionLogic
{
    public class CoffeeCap : Holdable, Iinteractable
    {
        [SerializeField]
        private Vector3 _holdPos;

        [SerializeField]
        private Vector3 _holdRot;

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent<CoffeeCup>(out var cup))
            {
                if (cup.IsFilled && !cup.IsCapEquiped)
                {
                    cup.EnableCap();
                    gameObject.SetActive(false);
                }
            }
            else if (other.gameObject.TryGetComponent<CoffeeNpc>(out var npc))
            {
                if (cup.IsFilled && cup.IsCapEquiped)
                {
                    npc.OnCoffeeCapCollision();
                }
            }
        }

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
    }
}