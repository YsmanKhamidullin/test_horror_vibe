using System;
using UnityEngine;

namespace Game.Core.InteractionLogic
{
    public class CollisionListener : MonoBehaviour
    {
        public event Action<Collider, Collision> OnCollisionEnterEvent;

        private Collider _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }

        private void OnCollisionEnter(Collision other)
        {
            OnCollisionEnterEvent?.Invoke(_collider, other);
        }
    }
}