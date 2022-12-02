using System.Collections;
using Audio;
using Commons;
using Commons.Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class EndGameScreen : MonoBehaviour
    {
        private static readonly int GrayScale = Shader.PropertyToID("_GrayScale");
        [SerializeField] private Image imageElement;
        [SerializeField] private TMP_Text playerNameText;
        [SerializeField] private Sprite victorySprite;
        [SerializeField] private Sprite defeatSprite;
        [SerializeField] private Sprite drawSprite;
        [SerializeField] private GameObject[] objectsToShow;
        [SerializeField] private GameEventChannel gameEventChannel;
        [SerializeField] private Material grayScaleMaterial;
        [SerializeField] private float grayScaleTransitionSpeed;
        [SerializeField] private AudioEventChannel audioEventChannel;
        [SerializeField] private GameInformation gameInformation;
        private int _currentPlayerNumber;

        private void Start()
        {
            grayScaleMaterial.SetFloat(GrayScale, 0);
        }

        private void OnEnable()
        {
            gameEventChannel.OnGameEnd += OnGameEnd;
            gameEventChannel.OnTurnChange += OnTurnChange;
        }

        private void OnTurnChange(int playerNumber)
        {
            _currentPlayerNumber = playerNumber;
        }

        private void OnDisable()
        {
            gameEventChannel.OnGameEnd -= OnGameEnd;
            gameEventChannel.OnTurnChange -= OnTurnChange;
            grayScaleMaterial.SetFloat(GrayScale, 0);
        }


        private void OnGameEnd()
        {
            bool isAWin;
            if (gameInformation.IsMultiplayer())
            {
                isAWin = gameInformation.playerCoins[0] != gameInformation.playerCoins[1];
                imageElement.sprite = isAWin ? victorySprite : drawSprite;
                playerNameText.text = _currentPlayerNumber > 0 ? $"¡Player {_currentPlayerNumber} Wins!" : "";
            }
            else
            {
                isAWin = gameInformation.playerCoins[0] > 0;
                imageElement.sprite = isAWin ? victorySprite : defeatSprite;
            }
                         
            foreach (var gameObjectToShow in objectsToShow) gameObjectToShow.SetActive(true);
            StartCoroutine(GrayScaleFade());
            audioEventChannel.ReproduceAudio(isAWin ? AudioClipGroupName.EndGameWin : AudioClipGroupName.EndGameLost);
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