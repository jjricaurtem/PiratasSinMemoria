using System;
using Commons.Data;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

namespace Card
{
    public class Card : MonoBehaviour, IPointerClickHandler
    {
        private static readonly int Start = Animator.StringToHash("Start");
        [SerializeField] private CardEventChannel cardEventChannel;
        [SerializeField] private int cardId;
        [SerializeField] private AudioClip[] cardClickedAudioClips;

        private Animator _animator;
        private AudioSource _audioSource;
        private CardData _cardData;
        private SpriteRenderer _frontSpriteRenderer;
        private bool _isCardUp;
        private bool _matched;
        private SpriteRenderer[] _spriteRenderers;
        private static readonly int TurnCardSideUp = Animator.StringToHash("TurnCardSideUp");
        private static readonly int TurnCardSideDown = Animator.StringToHash("TurnCardSideDown");
        private static readonly int RemoveCard = Animator.StringToHash("RemoveCard");

        private void OnEnable()
        {
            _animator = GetComponent<Animator>();
            _audioSource = GetComponent<AudioSource>();
            if (cardId < 0) throw new Exception("No index defined for this card " + name);
            _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
            _frontSpriteRenderer = GetComponent<SpriteRenderer>();
            SetVisible(false);

            cardEventChannel.OnCardTurnedDown += OnCardTurnedDown;
            cardEventChannel.OnMarkCardsMatched += OnMarkCardsMatched;
        }

        private void OnDestroy()
        {
            cardEventChannel.OnCardTurnedDown -= OnCardTurnedDown;
            cardEventChannel.OnMarkCardsMatched -= OnMarkCardsMatched;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_matched || _isCardUp) return;
            _audioSource.clip = cardClickedAudioClips[Random.Range(0, cardClickedAudioClips.Length)];
            _audioSource.Play();
            _animator.SetTrigger(TurnCardSideUp);
            _isCardUp = true;
        }

        public void SetVisible(bool visible)
        {
            foreach (var spriteRender in _spriteRenderers) spriteRender.enabled = visible;
        }

        public void InitialData(CardData cardData)
        {
            _cardData = cardData;
            _frontSpriteRenderer.sprite = _cardData.CardSo.sprite;
            cardId = cardData.CardId;
        }

        public void Initialize()
        {
            _animator.SetTrigger(Start);
        }

        [UsedImplicitly]
        public void CardAnimationStarted()
        {
            SetVisible(true);
            _audioSource.Play();
        }

        private void OnCardTurnedDown()
        {
            if (_matched || !_isCardUp) return;
            _animator.SetTrigger(TurnCardSideDown);
            _isCardUp = false;
        }

        private void OnMarkCardsMatched(string cardName)
        {
            if (!_cardData.CardSo.cardName.Equals(cardName) && !_matched) return;
            _matched = true;
            _animator.SetTrigger(RemoveCard);
        }

        [UsedImplicitly]
        public void OnAnimationEnds(string animationName)
        {
            switch (animationName)
            {
                case "RemoveCard":
                    enabled = false;
                    break;
                case "TurnCardSideUp":
                    cardEventChannel.OnCardTurnedUp?.Invoke(_cardData);
                    break;
            }
        }
    }
}