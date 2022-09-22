using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Commons.Data;
using Commons.Events;
using UnityEngine;

namespace Game
{
    public class Table : MonoBehaviour
    {
        [SerializeField] private CardSo[] availableCards;
        [SerializeField] private CardEventChannel cardEventChannel;
        [SerializeField] private GameEventChannel gameEventChannel;
        [SerializeField] private int cardsAmount;
        [SerializeField] private float cardDealSpeedInSeconds;
        [SerializeField] private AudioClip[] errorAudioClips;
        [SerializeField] private AudioClip[] matchAudioClips;
        private AudioSource _audioSource;
        private Card.Card[] _cards;
        private int _cardsMatched;
        private int _cardsReady;
        private CardData _currentCardUp;
        private bool _isTableInitialized;

        // Use this for initialization
        private void Start()
        {
            _cardsMatched = 0;
            _cards = GetComponentsInChildren<Card.Card>();
            _audioSource = GetComponent<AudioSource>();
            ResetBoard();
        }

        private void OnEnable()
        {
            cardEventChannel.OnCardTurnedUp += OnCardTurnedUp;
            cardEventChannel.OnCardReady += OnCardReady;
        }

        private void OnDestroy()
        {
            cardEventChannel.OnCardTurnedUp -= OnCardTurnedUp;
            cardEventChannel.OnCardReady -= OnCardReady;
        }

        private void ResetBoard()
        {
            foreach (var card in _cards) card.SetVisible(false);
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

            for (var i = 0; i < _cards.Length; i++) _cards[i].InitialData(new CardData(i, shuffledCards[i]));
        }

        private IEnumerator DealCards()
        {
            foreach (var card in _cards)
            {
                card.Initialize();
                yield return new WaitForSeconds(cardDealSpeedInSeconds);
            }
        }

        private void OnCardTurnedUp(CardData cardData)
        {
            if (_currentCardUp == null)
            {
                cardEventChannel.CardsInteractionActive(true);
                _currentCardUp = cardData;
            }
            else
            {
                var currentCardName = _currentCardUp.CardSo.cardName;
                cardEventChannel.CardsInteractionActive(false);
                if (currentCardName.Equals(cardData.CardSo.cardName))
                {
                    _cardsMatched += 2;
                    cardEventChannel.OnMarkCardsMatched?.Invoke(currentCardName);
                    _audioSource.clip = matchAudioClips[Random.Range(0, matchAudioClips.Length)];
                    if (_cardsMatched >= cardsAmount) gameEventChannel.GameEnd(true);
                }
                else
                {
                    cardEventChannel.OnCardTurnedDown?.Invoke();
                    _audioSource.clip = errorAudioClips[Random.Range(0, errorAudioClips.Length)];
                }

                _audioSource.Play();
                _currentCardUp = null;
            }
        }

        private void OnCardReady()
        {
            _cardsReady++;
            if (_isTableInitialized) return;
            if (_cardsReady < cardsAmount) return;
            cardEventChannel.CardsInteractionActive(true);
            _isTableInitialized = true;
            _cardsReady = 0;
        }
    }
}