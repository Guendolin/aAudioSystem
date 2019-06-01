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
        private float _lastLoopTime = -1f;

        private aAudioClip _currentClipp;
        private float _currentVolume;

        public bool IsActive()
        {
            return _active;
        }

        public void Update()
        {
            //Debug.Log(gameObject.name + "  isPlaying=" + audioSource.isPlaying + " => " + audioSource.time);
            switch (_playType)
            {
                case PlayType.Clip:
                    UpdateClip();
                    break;
                case PlayType.Loop:
                    UpdateLoop();
                    break;
                case PlayType.None:
                default:
                    _active = false;
                    break;
            }

            if (!_active)
            {
                _playType = PlayType.None;
                if (audioSource.isPlaying)
                {
                    audioSource.Stop();
                }
            }
        }

        public aAudioSource PlayClip(aAudioClip audioClip, float volume = 1f, float pitch = 0f, float pan = 0f)
        {
            _currentClipp = audioClip;
            audioSource.clip = audioClip.audioClip;
            audioSource.loop = false;
            audioSource.Play();

            _playType = PlayType.Clip;
            _active = true;

            audioSource.volume = _currentVolume = _currentClipp.volume * volume;
            audioSource.pitch = _currentClipp.pitch + pitch;
            audioSource.panStereo = _currentClipp.pan + pan;

            return this;
        }

        private void UpdateClip()
        {
            _active = audioSource.isPlaying;
        }

        public aAudioSource PlayLoop(aAudioClip audioClip, float volume = 1f, float pitch = 0f, float pan = 0f)
        {
            if(_playType != PlayType.Loop && !_active)
            {
                _currentClipp = audioClip;
                audioSource.clip = audioClip.audioClip;
                audioSource.loop = true;
                audioSource.Play();
                audioSource.volume = audioClip.volume;
                _playType = PlayType.Loop;
                _active = true;
            }

            audioSource.volume = _currentVolume = _currentClipp.volume * volume;
            audioSource.pitch = _currentClipp.pitch + pitch;
            audioSource.panStereo = _currentClipp.pan + pan;

            _lastLoopTime = Time.time;

            return this;
        }

       private void UpdateLoop()
       {
            float vMult = 1f-Mathf.Clamp01(Time.time - _lastLoopTime);
            audioSource.volume = _currentVolume * vMult;
            if(Mathf.Approximately(vMult, 0f))
            {
                _active = false;
            }
       }
    }
}
