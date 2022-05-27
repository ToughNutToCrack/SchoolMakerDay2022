using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    public AudioSource source;
    public AudioClip poke;
    public AudioClip pika;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

    }

    public void PlayPokeSound()
    {
        source.PlayOneShot(poke);
    } 

    public void PlayPikaSound()
    {
        source.PlayOneShot(pika);
    }
}
