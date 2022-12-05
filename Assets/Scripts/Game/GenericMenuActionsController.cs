using Audio;
using Commons;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GenericMenuActionsController : MonoBehaviour
    {
        [SerializeField] private AudioEventChannel effectsAudioEventChannel;
        [SerializeField] private GameInformation gameInformation;

        public void OpenUrl(string url)
        {
            effectsAudioEventChannel.ReproduceAudio(AudioClipGroupName.Click);
            Application.OpenURL(url);
        }

        public void LoadScene(string sceneName) {
            effectsAudioEventChannel.ReproduceAudio(AudioClipGroupName.Click);
            SceneManager.LoadScene(sceneName);
        }

        public void StartGame(int numberOfPlayers)
        {
            gameInformation.numberOfPlayers = numberOfPlayers;
            LoadScene("GameScene");
        }
        
    }
}