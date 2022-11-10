using System.Linq;
using UnityEngine;

namespace Audio
{
    [CreateAssetMenu(fileName = "AudioClipGroups", menuName = "MemoryLessPirates/Data/AudioClipGroups", order = 0)]
    public class AudioClipGroups : ScriptableObject
    {
        [SerializeField] private AudioClipGroup[] audioClipGroups;

        public AudioClipGroup GetByGroupName( AudioClipGroupName groupName)
        {
            return audioClipGroups.FirstOrDefault(audioClipGroup => audioClipGroup.ClipGroupName == groupName);
        }
    }
}