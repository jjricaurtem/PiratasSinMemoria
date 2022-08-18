﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Table : MonoBehaviour
{
    [SerializeField] private CardSo[] availableCards;
    [SerializeField] private CardEventChannel cardEventChannel;
    [SerializeField] private GameEventChannel gameEventChannel;
    [SerializeField] private int cardsAmount;
    [SerializeField] private float cardDealSpeedInSeconds;
    [SerializeField] private AudioClip[] errorAudioClips;
    [SerializeField] private AudioClip[] matchAudioClips;
    private int _cardsMatched;
    private Card[] _cards;
    private CardData _currentCardUp;
    private AudioSource _audioSource;

    // Use this for initialization
    void Start()
    {
        _cardsMatched = 0;
        _cards = GetComponentsInChildren<Card>();
        _audioSource = GetComponent<AudioSource>();
        ResetBoard();
    }

    private void ResetBoard()
    {
        foreach (Card card in _cards) card.SetVisible(false);
        RandomizeCards();
        StartCoroutine(DealCards());

    }

    private void RandomizeCards()
    {
        IList<CardSo> finalCards = new List<CardSo>(cardsAmount);
        IList<CardSo> temporalAvailableCards = new List<CardSo>(availableCards);

        for (int i = 0; i < cardsAmount; i += 2)
        {
            int availableCardIndex = Random.Range(0, temporalAvailableCards.Count);
            var cardSo = temporalAvailableCards[availableCardIndex];
            finalCards.Add(cardSo);
            finalCards.Add(cardSo);
            temporalAvailableCards.RemoveAt(availableCardIndex);
        }
        var shuffledCards = finalCards.OrderBy(a => Random.value).ToList();

        for (int i = 0; i < _cards.Length; i++) _cards[i].InitialData(new CardData(i, shuffledCards[i]));
    }

    IEnumerator DealCards()
    {
        foreach (Card card in _cards)
        {
            card.Initilize();
            yield return new WaitForSeconds(cardDealSpeedInSeconds);
        }
    }

    private void OnCardTurnedUp(CardData cardData)
    {
        if (_currentCardUp == null) _currentCardUp = cardData;
        else
        {
            var currentCardName = _currentCardUp.CardSo.cardName;
            if (currentCardName.Equals(cardData.CardSo.cardName))
            {
                _cardsMatched += 2;
                cardEventChannel.OnMarkCardsMatched?.Invoke(currentCardName);
                _audioSource.clip = matchAudioClips[Random.Range(0, matchAudioClips.Length)];
                if(_cardsMatched >= cardsAmount) gameEventChannel.GameEnd(true);
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

    private void OnEnable() => cardEventChannel.OnCardTurnedUp += OnCardTurnedUp;
    private void OnDestroy() => cardEventChannel.OnCardTurnedUp -= OnCardTurnedUp;

}
