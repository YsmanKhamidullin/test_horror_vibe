using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.Core.Architecture.Services.Base;
using Game.Core.Utils;
using NaughtyAttributes;
using UnityEngine;

namespace Game.Core.Architecture.Services
{
    public class AudioService : MonoBehaviour
    {
        // private AudioClip Money => GameResources._AudioClip.A_MiniGameWin;

        [SerializeField]
        private List<AudioClip> _keyClips;

        [SerializeField]
        private AudioSource _key;

        [SerializeField]
        private AudioSource _keySecond;


        [Button]
        public void PlayKey()
        {
            if (_key.isPlaying)
            {
                if (_keySecond.isPlaying)
                {
                    return;
                }

                var v1 = Random.Range(0.85f, 1.15f);
                _keySecond.pitch = v1;
                _keySecond.clip = _keyClips.GetRandom();
                _keySecond.Play();
                return;
            }

            var v = Random.Range(0.85f, 1.15f);
            _key.pitch = v;
            _key.clip = _keyClips.GetRandom();
            _key.Play();
        }
    }
}