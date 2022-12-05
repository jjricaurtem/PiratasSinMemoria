using System;
using Audio;
using Commons.Events;
using JetBrains.Annotations;
using Tables;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Cards
{
    public class Card : MonoBehaviour, IPointerClickHandler
    {
        private static readonly int Start = Animator.StringToHash("Start");
        private static readonly int TurnCardSideUp = Animator.StringToHash("TurnCardSideUp");
        private static readonly int TurnCardSideDown = Animator.StringToHash("TurnCardSideDown");
        private static readonly int RemoveCard = Animator.StringToHash("RemoveCard");
        public TableEventChannel tableEventChannel;
        [SerializeField] private AudioEventChannel audioEventChannelFx;

        private Animator _animator;
        private int _cardIndex;
        private CardSo _cardSo;
        private SpriteRenderer _frontSpriteRenderer;
        private bool _interactionEnable;
        private bool _isCardUp;
        private bool _isSelected;
        private SpriteRenderer[] _spriteRenderers;
        private Table _table;

        public bool Matched { get; private set; }

        private void OnEnable()
        {
            _animator = GetComponent<Animator>();
            if (_cardIndex < 0) throw new Exception("No index defined for this card " + name);
            _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
            _frontSpriteRenderer = GetComponent<SpriteRenderer>();
            SetVisible(false);

            tableEventChannel.OnCardsInteractionActivation += OnCardsInteractionActivation;
        }

        private void OnDestroy()
        {
            tableEventChannel.OnCardsInteractionActivation -= OnCardsInteractionActivation;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (Matched || _isCardUp || !_interactionEnable) return;
            audioEventChannelFx.ReproduceAudio(AudioClipGroupName.Click);
            tableEventChannel.SetCardsInteractionActive(false);
            _animator.SetTrigger(TurnCardSideUp);
            _isCardUp = true;
        }

        public void SetVisible(bool visible)
        {
            foreach (var spriteRender in _spriteRenderers) spriteRender.enabled = visible;
        }

        public void InitialData(int index, CardSo cardSo, Table table)
        {
            _cardSo = cardSo;
            _frontSpriteRenderer.sprite = _cardSo.sprite;
            _cardIndex = index;
            _table = table;
        }

        public void Initialize()
        {
            _animator.SetTrigger(Start);
        }

        [UsedImplicitly]
        public void CardAnimationStarted()
        {
            SetVisible(true);
            audioEventChannelFx.ReproduceAudio(AudioClipGroupName.CardDeal);
        }

        public void TurnCardDown()
        {
            if (Matched || !_isCardUp) return;
            _animator.SetTrigger(TurnCardSideDown);
            _isCardUp = false;
        }

        public void MarkCardAsMatched()
        {
            if (Matched) return;
            Matched = true;
            _animator.SetTrigger(RemoveCard);
        }

        public void OnAnimationEnds(string animationName) => OnAnimationEnds(Animator.StringToHash(animationName));

        private void OnAnimationEnds(int animationNameHash)
        {
            if (animationNameHash == TurnCardSideUp)
            {
                _table.OnCardTurnedUp(_cardIndex);
            }
            else
            {
                _table.OnCardReady();
                // if (animationNameHash == RemoveCard) enabled = false;
                if (animationNameHash == RemoveCard || animationNameHash == TurnCardSideDown)
                    tableEventChannel.SetCardsInteractionActive(true);
            }
        }

        public bool IsSameCardSo(Card card) => _cardSo.name.Equals(card._cardSo.name);

        private void OnCardsInteractionActivation(bool active) => _interactionEnable = active;
    }
}