using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cards;
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
        [SerializeField] private AudioClip[] errorAudioClips;
        [SerializeField] private AudioClip[] matchAudioClips;
        private AudioSource _audioSource;
        private int _cardsMatched;
        private int _cardsReady;
        private Coins _coins;
        private int _currentCardUpIndex = -1;
        private bool _isTableInitialized;

        public Card[] Cards { get; private set; }

        // Use this for initialization
        private void Start()
        {
            _cardsMatched = 0;
            Cards = GetComponentsInChildren<Cards.Card>();
            _audioSource = GetComponent<AudioSource>();
            _coins = GetComponentInChildren<Coins>();
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
                    _audioSource.clip = matchAudioClips[Random.Range(0, matchAudioClips.Length)];
                    if (_cardsMatched >= cardsAmount) gameEventChannel.GameEnd(true);
                }
                else
                {
                    currentCardUp.TurnCardDown();
                    newCardUp.TurnCardDown();
                    _coins.RemoveCoin();
                    _audioSource.clip = errorAudioClips[Random.Range(0, errorAudioClips.Length)];
                }

                _audioSource.Play();
                _currentCardUpIndex = -1;
            }
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