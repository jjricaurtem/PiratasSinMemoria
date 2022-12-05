using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Audio
{
    public class AudioEmitterPool : MonoBehaviour
    {
        private const int InitialPoolSize = 8;
        [SerializeField] private AudioEmitter audioEmitterPrefab;
        [SerializeField] private AudioEventChannel audioEventChannel;
        private List<AudioEmitter> _audioEmitters;

        private void Start()
        {
            _audioEmitters = new List<AudioEmitter>(InitialPoolSize);
            for (var i = 0; i < InitialPoolSize; i++) CreateNewAudioEmitter();
        }

        private void OnEnable()
        {
            audioEventChannel.OnReproduceSound += OnReproduceSound;
        }

        private void OnDisable() => audioEventChannel.OnReproduceSound -= OnReproduceSound;


        private void OnReproduceSound(AudioClipGroup audioClipGroup)
        {
            var audioEmitter = GetFreeAudioEmitter();
            audioEmitter.ReproduceClip(audioClipGroup.GetRandomClip());
        }

        private AudioEmitter GetFreeAudioEmitter()
        {
            foreach (var audioEmitter in _audioEmitters.Where(audioEmitter => !audioEmitter.IsPlaying))
                return audioEmitter;
            return CreateNewAudioEmitter();
        }

        private AudioEmitter CreateNewAudioEmitter()
        {
            var newAudioEmitter = Instantiate(audioEmitterPrefab, transform);
            _audioEmitters.Add(newAudioEmitter);
            return newAudioEmitter;
        }
    }
}