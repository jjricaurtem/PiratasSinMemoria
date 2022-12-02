using System;
using Commons;
using Commons.Events;
using TMPro;
using UnityEngine;

namespace Game
{
    public class CurrentPlayerTurnIndicator : MonoBehaviour
    {
        [SerializeField] private GameInformation gameInformation;
        [SerializeField] private GameEventChannel gameEventChannel;
        [SerializeField] private TMP_Text playerNameText;

        private void Start()
        {
            gameObject.SetActive(gameInformation.numberOfPlayers > 1);
        }

        private void OnEnable() => gameEventChannel.OnTurnChange += OnTurnChange;
        private void OnDisable() => gameEventChannel.OnTurnChange -= OnTurnChange;

        private void OnTurnChange(int currentPlayerNumber)
        {
            playerNameText.text = $"Player {currentPlayerNumber}";
        }
    }
}