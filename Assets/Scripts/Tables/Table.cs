using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Audio;
using Cards;
using Commons;
using Commons.Events;
using UnityEngine;

namespace Tables
{
    public class Table : MonoBehaviour
    {
        [SerializeField] private CardSo[] availableCards;
        [SerializeField] private TableEventChannel tableEventChannel;
        [SerializeField] private GameEventChannel gameEventChannel;
        [SerializeField] private int cardsAmount;
        [SerializeField] private float cardDealSpeedInSeconds;
        [SerializeField] private GameInformation gameInformation;
        [SerializeField] private GameObject gun;
        [SerializeField] private AudioEventChannel audioEventChannelFx;
        private int _cardsMatched;
        private int _cardsReady;
        private int _currentCardUpIndex = -1;
        private int _currentTurn = 1;
        private bool _isTableInitialized;

        private Card[] Cards { get; set; }

        private void Start()
        {
            _cardsMatched = 0;
            Cards = GetComponentsInChildren<Card>();
            gun.SetActive(!gameInformation.IsMultiplayer());
            ResetBoard();
        }

        public void SelectCard(int index)
        {
            Cards[index].OnPointerClick(null);
        }

        public void OnCardTurnedUp(int cardIndex)
        {
            if (_currentCardUpIndex < 0)
            {
                tableEventChannel.SetCardsInteractionActive(true);
                _currentCardUpIndex = cardIndex;
            }
            else
            {
                var currentCardUp = Cards[_currentCardUpIndex];
                var newCardUp = Cards[cardIndex];
                tableEventChannel.SetCardsInteractionActive(false);
                if (newCardUp.IsSameCardSo(currentCardUp))
                {
                    _cardsMatched += 2;
                    currentCardUp.MarkCardAsMatched();
                    newCardUp.MarkCardAsMatched();
                    audioEventChannelFx.ReproduceAudio(AudioClipGroupName.CardMatch);
                    if (_cardsMatched >= cardsAmount)
                        gameEventChannel.GameEnd();
                }
                else
                {
                    currentCardUp.TurnCardDown();
                    newCardUp.TurnCardDown();

                    tableEventChannel.RemoveCoin(_currentTurn);
                    audioEventChannelFx.ReproduceAudio(AudioClipGroupName.CardError);
                }

                _currentCardUpIndex = -1;

                ChangeTurn();
            }
        }

        private void ChangeTurn()
        {
            if (!gameInformation.IsMultiplayer()) return;
            _currentTurn = _currentTurn == 1 ? 2 : 1;
            gameEventChannel.ChangeTurn(_currentTurn);
        }

        public void OnCardReady()
        {
            _cardsReady++;
            if (_isTableInitialized) return;
            if (_cardsReady < cardsAmount) return;
            tableEventChannel.SetCardsInteractionActive(true);
            _isTableInitialized = true;
            _cardsReady = 0;
        }

        private void ResetBoard()
        {
            foreach (var card in Cards) card.SetVisible(false);
            _cardsReady = 0;
            _isTableInitialized = false;
            RandomizeCards();
            StartCoroutine(DealCards());
        }

        private void RandomizeCards()
        {
            IList<CardSo> finalCards = new List<CardSo>(cardsAmount);
            IList<CardSo> temporalAvailableCards = new List<CardSo>(availableCards);

            for (var i = 0; i < cardsAmount; i += 2)
            {
                var availableCardIndex = Random.Range(0, temporalAvailableCards.Count);
                var cardSo = temporalAvailableCards[availableCardIndex];
                finalCards.Add(cardSo);
                finalCards.Add(cardSo);
                temporalAvailableCards.RemoveAt(availableCardIndex);
            }

            var shuffledCards = finalCards.OrderBy(_ => Random.value).ToList();

            for (var i = 0; i < Cards.Length; i++) Cards[i].InitialData(i, shuffledCards[i], this);
        }

        private IEnumerator DealCards()
        {
            foreach (var card in Cards)
            {
                card.Initialize();
                yield return new WaitForSeconds(cardDealSpeedInSeconds);
            }
        }
    }
}