using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace aSystem.aAudioSystem
{
    public class aAudioSystem : MonoBehaviour
    {
        public static aAudioSystem Instance;

        private int _audioSourceNumber = 0;
        private Queue<aAudioSource> _audioSourceQueue;
        private List<aAudioSource> _activeAudioSource;

        private const string AUDIO_SOURCE_NAME = "aAudioSource-{0}";

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            _audioSourceQueue = new Queue<aAudioSource>();
            _activeAudioSource = new List<aAudioSource>();
        }

        void Update()
        {
            if(_activeAudioSource.Count > 0)
            {
                for(int i = _activeAudioSource.Count-1; i >= 0; i--)
                {
                    aAudioSource aAudioSource = _activeAudioSource[i];
                    //Debug.Log("checking " + aAudioSource.gameObject.name + " IsActive=" + aAudioSource.IsActive());
                    if (!aAudioSource.IsActive())
                    {
                        ReturnAudioSource(aAudioSource);
                    }
                }
            }
        }

        private aAudioSource CreateAudioSource()
        {
            // Create game object
            GameObject gameObject = new GameObject();
            gameObject.transform.parent = transform;
            gameObject.transform.position = transform.position;
            gameObject.name = string.Format(AUDIO_SOURCE_NAME, _audioSourceNumber);

            // Create native audio surce
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;

            // Create system audio source
            aAudioSource aAudioPlayer = gameObject.AddComponent<aAudioSource>();
            aAudioPlayer.audioSource = audioSource;

            _audioSourceNumber++;

            return aAudioPlayer;
        }

        private aAudioSource GetAudioSoruce()
        {
            if (_audioSourceQueue.Count == 0)
            {
                aAudioSource audioSource = CreateAudioSource();
                _activeAudioSource.Add(audioSource);
                return audioSource;
            }
            else
            {
                aAudioSource audioSource = _audioSourceQueue.Dequeue();
                audioSource.gameObject.SetActive(true);
                _activeAudioSource.Add(audioSource);
                return audioSource;
            }
        }

        private void ReturnAudioSource(aAudioSource audioSource)
        {
            audioSource.gameObject.SetActive(false);
            _activeAudioSource.Remove(audioSource);
            _audioSourceQueue.Enqueue(audioSource);
        }

        public aAudioSource PlaySound(aAudioClip audioClip)
        {
            aAudioSource aAudioSource = GetAudioSoruce();
            aAudioSource.PlayClip(audioClip);
            return aAudioSource;
        }

        public aAudioSource PlayLoop(aAudioClip audioClip, aAudioSource aAudioSource, float volume = 1f, float pitch = 0f, float pan = 0f)
        {
            if(aAudioSource == null || !aAudioSource.IsActive())
            {
                aAudioSource = GetAudioSoruce();
            }
    
            aAudioSource.PlayLoop(audioClip, volume, pitch, pan);

            return aAudioSource;
        }
    }
}


