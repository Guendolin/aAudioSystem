using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace aSystem.aAudioSystem
{
    [CreateAssetMenu(fileName = "aAdioClip", menuName = "aAudioSystem/aAudioClip")]
    public class aAudioClip : ScriptableObject
    {
        public AudioClip audioClip;
        [Space]
        [Range(0f, 1f)]
        public float volume = 1f;
        [Range(-3f, 3f)]
        public float pitch = 0f;
        [Range(-1f, 1f)]
        public float pan = 0f;
    }
}
