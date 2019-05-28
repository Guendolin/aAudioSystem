using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace aSystem.aAudioSystem
{
    [CreateAssetMenu(fileName = "aAdioClip", menuName = "aAudioSystem/aAudioClip")]
    public class aAudioClip : ScriptableObject
    {
        public AudioClip audioClip;
    }
}
