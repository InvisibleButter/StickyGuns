using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

namespace StickyGuns.Sound
{
    public class AudioManager : MonoBehaviour
    {

        public Sound[] sounds;

        void Awake()
        {

            foreach (Sound sound in sounds)
            {
                sound.source = gameObject.AddComponent<AudioSource>();
                sound.source.clip = sound.clip;
                sound.source.volume = sound.volume;
                sound.source.pitch = sound.pitch;
                sound.source.loop = sound.loop;
                sound.source.outputAudioMixerGroup = sound.mixer;
            }
        }


        private void Start()
        {
            Play("StartMelodie");
        }

        public void Play(string name)
        {
            Sound sound = Array.Find(sounds, sound => sound.name == name);

            if (sound == null)
            {
                return;
            }

            sound.source.Play();
        }

    }
}
