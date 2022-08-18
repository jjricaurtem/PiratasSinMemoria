using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private CardEventChannel cardEventChannel;
    [SerializeField] private int cardId;
    [SerializeField] private AudioClip[] cardClickedAudioClips;

    private Animator _animator;
    private AudioSource _audioSource;
    private SpriteRenderer[] _spriteRenderers;
    private SpriteRenderer _frontSpriteRenderer;
    private bool _matched = false;
    private bool _isCardUp = false;
    private CardData _cardData;

    public void SetVisible(bool visible)
    {
        foreach (SpriteRenderer spriteRender in _spriteRenderers) spriteRender.enabled = visible;
    }

    public Card InitialData(CardData cardData)
    {
        _cardData = cardData;
        _frontSpriteRenderer.sprite = _cardData.CardSo.sprite;
        cardId = cardData.cardId;
        return this;
    }

    public void Initilize()
    {
        _animator.SetTrigger("Start");
    }

    public void CardAnimationStarted()
    {
        SetVisible(true);
        _audioSource.Play();
    }

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

    private void OnCardTurnedDown()
    {
        if (_matched || !_isCardUp) return;
        _animator.SetTrigger("TurnCardSideDown");
        _isCardUp = false;
    }

    private void OnDestroy()
    {
        cardEventChannel.OnCardTurnedDown -= OnCardTurnedDown;
        cardEventChannel.OnMarkCardsMatched -= OnMarkCardsMatched;
    }

    private void OnMarkCardsMatched(string cardName)
    {
        if (!_cardData.CardSo.cardName.Equals(cardName) && !_matched) return;
        _matched = true;
        _animator.SetTrigger("RemoveCard");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_matched || _isCardUp) return;
        _audioSource.clip = cardClickedAudioClips[UnityEngine.Random.Range(0, cardClickedAudioClips.Length)];
        _audioSource.Play();
        _animator.SetTrigger("TurnCardSideUp");
        _isCardUp = true;
    }

    public void OnAnimationEnds(string animationName)
    {
        if ("RemoveCard".Equals(animationName))
        {
            enabled = false;
        }
        else if ("TurnCardSideUp".Equals(animationName))
        {
            cardEventChannel.OnCardTurnedUp?.Invoke(_cardData);
        }
    }
}
