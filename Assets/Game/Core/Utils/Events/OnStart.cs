using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Game.Core.Utils.Events
{
    public class OnStart : MonoBehaviour
    {
        public UnityEvent OnStartEvent;

        private void Start()
        {
            OnStartEvent?.Invoke();
        }
    }
}