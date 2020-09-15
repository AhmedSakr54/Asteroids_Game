using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioManager
{
    static AudioSource audioSource;
    static Dictionary<AudioClipName, AudioClip> audioClips = new Dictionary<AudioClipName, AudioClip>();


    public static void Initialize(AudioSource source)
    {
        audioSource = source;
        audioClips.Add(AudioClipName.AsteroidHit,
                       Resources.Load<AudioClip>("hit"));
        audioClips.Add(AudioClipName.PlayerDeath,
                       Resources.Load<AudioClip>("die"));
        audioClips.Add(AudioClipName.PlayerShot,
                       Resources.Load<AudioClip>("shoot"));
    }

    public static void Play(AudioClipName name)
    {
        audioSource.PlayOneShot(audioClips[name]);
    }

}
