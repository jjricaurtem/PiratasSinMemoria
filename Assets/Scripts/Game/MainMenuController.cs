using Audio;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private AudioEventChannel musicAudioEventChannel;
        [SerializeField] private AudioEventChannel effectsAudioEventChannel;
        private void Start()
        {
            musicAudioEventChannel.ReproduceAudio(AudioClipGroupName.Intro);
        }

        public void OpenUrl(string url)
        {
            effectsAudioEventChannel.ReproduceAudio(AudioClipGroupName.Click);
            Application.OpenURL(url);
        }

        public void LoadScene(string sceneName) {
            effectsAudioEventChannel.ReproduceAudio(AudioClipGroupName.Click);
            SceneManager.LoadScene(sceneName);
        }
        
    }
}