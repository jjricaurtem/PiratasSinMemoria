using System.Collections;
using Commons.Events;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class EndGameScreen : MonoBehaviour
    {
        private static readonly int GrayScale = Shader.PropertyToID("_GrayScale");
        [SerializeField] private Image imageElement;
        [SerializeField] private Sprite victorySprite;
        [SerializeField] private Sprite defeatSprite;
        [SerializeField] private AudioClip[] winAudioClips;
        [SerializeField] private AudioClip[] loseAudioClips;
        [SerializeField] private GameObject[] objectsToShow;
        [SerializeField] private GameEventChannel gameEventChannel;
        [SerializeField] private Material grayScaleMaterial;
        [SerializeField] private float grayScaleTransitionSpeed;
        private AudioSource _audioSource;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            grayScaleMaterial.SetFloat(GrayScale, 0);
        }

        private void OnEnable() => gameEventChannel.OnGameEnd += OnGameEnd;
        private void OnDisable()
        {
            gameEventChannel.OnGameEnd -= OnGameEnd;
            grayScaleMaterial.SetFloat(GrayScale, 0);
        }


        private void OnGameEnd(bool isAWin)
        {
            imageElement.sprite = isAWin ? victorySprite : defeatSprite;
            foreach (var gameObjectToShow in objectsToShow) gameObjectToShow.SetActive(true);

            if (!isAWin) StartCoroutine(GrayScaleFade());

            var audioClips = isAWin ? winAudioClips : loseAudioClips;
            _audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
            _audioSource.Play();
        }

        private IEnumerator GrayScaleFade()
        {
            float time = 0;
            var amount = 0f;
            while (amount < 1f)
            {
                time += Time.deltaTime * grayScaleTransitionSpeed;
                amount = Mathf.Lerp(0, 1, time);
                grayScaleMaterial.SetFloat(GrayScale, time);
                yield return null;
            }
        }
    }
}