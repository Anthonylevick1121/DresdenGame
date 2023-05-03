using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PausableAudio : MonoBehaviour
{
    private AudioSource audioSource;
    
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    private void OnEnable() => VoicePlayer.instance.OnAudioPause += OnPause;
    private void OnDisable() => VoicePlayer.instance.OnAudioPause -= OnPause;
    
    private void OnPause(bool paused)
    {
        if(paused) audioSource.Pause();
        else audioSource.UnPause();
    }
}
