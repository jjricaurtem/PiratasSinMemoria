﻿using System.Collections;
using Audio;
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
        [SerializeField] private GameObject[] objectsToShow;
        [SerializeField] private GameEventChannel gameEventChannel;
        [SerializeField] private Material grayScaleMaterial;
        [SerializeField] private float grayScaleTransitionSpeed;
        [SerializeField] private AudioEventChannel audioEventChannel;

        private void Start()
        {
            grayScaleMaterial.SetFloat(GrayScale, 0);
        }

        private void OnEnable() => gameEventChannel.OnGameEnd += OnGameEnd;

        private void OnDisable()
        {
            gameEventChannel.OnGameEnd -= OnGameEnd;
            grayScaleMaterial.SetFloat(GrayScale, 0);
        }


        private void OnGameEnd(bool isAWin, string playerName)
        {
            imageElement.sprite = isAWin ? victorySprite : defeatSprite;
            foreach (var gameObjectToShow in objectsToShow) gameObjectToShow.SetActive(true);
            playerNameText.text = playerName != null ? $"¡{playerName} Wins!" : "";

            if (!isAWin) StartCoroutine(GrayScaleFade());
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