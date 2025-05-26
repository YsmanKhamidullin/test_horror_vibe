using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

namespace Game.Core.Utils.Events
{
    public class OnVideoPlayerStarted : MonoBehaviour
    {
        public VideoPlayer VideoPlayer;
        public UnityEvent OnVideoPlayerStartedEvent;

        private void Awake()
        {
            VideoPlayer.started += Call;
        }

        private void Call(VideoPlayer source)
        {
            OnVideoPlayerStartedEvent?.Invoke();
        }

        private void OnDestroy()
        {
            VideoPlayer.started -= Call;
        }
    }
}