using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


namespace StickyGuns.Sound
{
    [System.Serializable]
    public class Sound
    {

        public string name;

        public AudioClip clip;
        public AudioMixerGroup mixer;

        [Range(0f, 1f)]
        public float volume = 0.1f;

        [Range(0.1f, 3f)]
        public float pitch = 1;

        public bool loop;

        [HideInInspector]
        public AudioSource source;

    }
}
