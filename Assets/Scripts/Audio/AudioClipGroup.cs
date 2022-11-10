using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Audio
{
    [Serializable]
    public class AudioClipGroup
    {
        [SerializeField] private AudioClipGroupName audioClipGroupName;
        [SerializeField] private bool isLoopTime;
        [SerializeField] private List<AudioClip> audioClips;

        public bool IsLoopTime => isLoopTime;

        public AudioClipGroupName ClipGroupName => audioClipGroupName;

        public AudioClip GetRandomClip() => audioClips[Random.Range(0, audioClips.Count)];
    }
}