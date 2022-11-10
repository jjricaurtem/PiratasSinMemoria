using UnityEngine;
using UnityEngine.Events;

namespace Audio
{
    [CreateAssetMenu(fileName = "NAME_AudioEventChannel", menuName = "MemoryLessPirates/Events/AudioEventChannel")]
    public class AudioEventChannel : ScriptableObject
    {
        public UnityAction<AudioClipGroup> OnReproduceSound;
        [SerializeField] private AudioClipGroups audioClipGroups;
        
        public void ReproduceAudio(AudioClipGroupName audioClipGroupName)
        {
            var audioClipGroup = audioClipGroups.GetByGroupName(audioClipGroupName);
            OnReproduceSound?.Invoke(audioClipGroup);
        }
    }
}