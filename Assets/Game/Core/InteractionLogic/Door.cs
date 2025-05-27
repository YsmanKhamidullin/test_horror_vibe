using System;
using UnityEngine;

namespace Game.Core.InteractionLogic
{
    public class Door : MonoBehaviour, Iinteractable
    {
        [SerializeField]
        private Animator _animator;

        private bool _isOpen;

        public void Clicked(Action removedFromHand = null)
        {
            _isOpen = !_isOpen;
            _animator.SetBool("IsOpen", _isOpen);
        }
    }
}