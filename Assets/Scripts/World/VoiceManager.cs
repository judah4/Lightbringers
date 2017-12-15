using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceManager : MonoBehaviour
{
    public static VoiceManager Instance;
    public AudioSource AudioSource;
    public List<AudioClip> Voices;

    // Use this for initialization
    void Awake()
    {
        Instance = this;
    }

    public void TriggerAudio(AudioClip clip)
    {
        AudioSource.clip = clip;
        AudioSource.Play();
    }

    public void TriggerVoice()
    {
        AudioSource.clip = Voices[Random.Range(0, Voices.Count)];
        AudioSource.Play();
    }
}
