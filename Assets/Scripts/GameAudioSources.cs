using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudioSources : MonoBehaviour
{
    void Awake()
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        AudioManager.Initialize(audioSource);
    }
}
