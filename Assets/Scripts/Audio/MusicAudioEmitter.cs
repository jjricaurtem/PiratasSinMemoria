﻿using UnityEngine;

namespace Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class MusicAudioEmitter : MonoBehaviour
    {
        [SerializeField] private AudioEventChannel audioEventChannel;
        private AudioSource _audioSource;

        private void OnEnable()
        {
            _audioSource = GetComponent<AudioSource>();
            if (_audioSource == null) _audioSource = gameObject.AddComponent<AudioSource>();
            audioEventChannel.OnReproduceSound += OnReproduceSound;
        }

        private void OnDisable() => audioEventChannel.OnReproduceSound -= OnReproduceSound;

        private void OnReproduceSound(AudioClipGroup audioClipGroup)
        {
            ReproduceClip(_audioSource, audioClipGroup.GetRandomClip(), audioClipGroup.IsLoopTime);
        }

        private static void ReproduceClip(AudioSource audioSource, AudioClip clip, bool isLoop)
        {
            audioSource.Stop();
            audioSource.loop = isLoop;
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}