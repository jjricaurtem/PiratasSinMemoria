using Audio;
using UnityEngine;

namespace Scenes
{
    public class SceneMusic : MonoBehaviour
    {
        [SerializeField] private AudioEventChannel musicAudioEventChannel;
        [SerializeField] private AudioClipGroupName audioClipGroupName;
        private void Start()
        {
            musicAudioEventChannel.ReproduceAudio(audioClipGroupName);
        }
    }
}