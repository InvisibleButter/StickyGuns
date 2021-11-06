using UnityEngine;
using System;
using UnityEngine.Audio;

namespace StickyGuns.Sound
{
    public class AudioManager : MonoBehaviour
    {

        public static AudioManager Instance;

        public AudioMixerGroup[] mixerGroups;
        public AudioSource[] audioSources;

        public Sound[] sounds;

        void Awake()
        {

            if (Instance != null)
            {
                Debug.Log("DUPLICATED AUDIO MANAGER DETECTED. Grüß Gott");
                Destroy(gameObject);
                return;
            }

            audioSources = new AudioSource[mixerGroups.Length];
            for (int i = 0; i < mixerGroups.Length; i++)
            {
                AudioSource audioSource = gameObject.AddComponent<AudioSource>();
                audioSources[i] = audioSource;
                //audioSource.outputAudioMixerGroup = mixerGroups[i];
            }

            Instance = this;
        }

        private void PlaySound(Sound sound)
        {
            if(sound.source == null)
            {
                for (int i = 0; i < mixerGroups.Length; i++)
                {
                    if (mixerGroups[i].audioMixer.name == sound.mixer.audioMixer.name)
                    {
                        sound.source = audioSources[i];
                        break;
                    }
                }
            }

            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
            sound.source.Stop();
            sound.source.Play();
        }

        public void Play(string name)
        {
             Sound sound = Array.Find(sounds, sound => sound.name == name);

            if (sound == null)
            {
                Debug.LogError("SOUND '" + name + "' not found");
                return;
            }

            PlaySound(sound);
        }

    }
}
