using System;
using Game.Core.Player;
using UnityEngine;

namespace Game.Core.Utils
{
    [RequireComponent(typeof(Collider))]
    public class PlayerTriggerListener : MonoBehaviour
    {
        public event Action<PlayerController> OnPlayerEnter;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player"))
            {
                return;
            }

            if (other.TryGetComponent<PlayerController>(out var p))
            {
                OnPlayerEnter?.Invoke(p);
            }
        }
    }
}