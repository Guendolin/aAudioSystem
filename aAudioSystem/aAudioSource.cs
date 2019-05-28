using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace aSystem.aAudioSystem
{
    public class aAudioSource : MonoBehaviour
    {
        [System.NonSerialized] public AudioSource audioSource;

        private enum PlayType { None = 0, Clip = 1, Loop = 2}
        private PlayType _playType;
        private bool _active;

        public bool IsActive()
        {
            return _active;
        }

        public void Update()
        {
            Debug.Log(gameObject.name + "  isPlaying=" + audioSource.isPlaying + " => " + audioSource.time);
            switch (_playType)
            {
                case PlayType.Clip:
                    _active = audioSource.isPlaying;
                    break;
                case PlayType.Loop:
                    break;
                case PlayType.None:
                default:
                    _active = false;
                    break;
            }

            if (!_active)
            {
                _playType = PlayType.None;
            }
        }

        public aAudioSource PlayClip(aAudioClip audioClip)
        {
            audioSource.clip = audioClip.audioClip;
            audioSource.loop = false;
            audioSource.Play();
            _playType = PlayType.Clip;
            _active = true;

            return this;
        }
    }
}
