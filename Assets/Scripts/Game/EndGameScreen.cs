using Commons.Events;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class EndGameScreen : MonoBehaviour
    {
        private AudioSource _audioSource;
        [SerializeField] private Image imageElement;
        [SerializeField] private Sprite victorySprite;
        [SerializeField] private Sprite defeatSprite;
        [SerializeField] private AudioClip[] winAudioClips;
        [SerializeField] private AudioClip[] loseAudioClips;
        [SerializeField] private GameObject[] objectsToShow;
        [SerializeField] private GameEventChannel gameEventChannel;

        private void OnEnable() => gameEventChannel.OnGameEnd += OnGameEnd;
        private void OnDisable() => gameEventChannel.OnGameEnd -= OnGameEnd;


        private void OnGameEnd(bool isAWin)
        {
            imageElement.sprite = isAWin ? victorySprite : defeatSprite;
            foreach (var gameObjectToShow in objectsToShow) gameObjectToShow.SetActive(true);

            var audioClips = isAWin ? winAudioClips : loseAudioClips;
            _audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
            _audioSource.Play();
        }

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }
    }
}
