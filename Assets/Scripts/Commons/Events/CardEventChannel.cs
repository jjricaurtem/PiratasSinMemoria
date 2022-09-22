using Commons.Data;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "CardEventChannel", menuName = "MemorylessPirates/Events/CardEventChannel")]
public class CardEventChannel : ScriptableObject
{
    public Card.Card currentCardUp;
    public UnityAction OnCardTurnedDown;
    public UnityAction<CardData> OnCardTurnedUp;
    public UnityAction<int> OnInitializationAnimationStarted;
    public UnityAction<string> OnMarkCardsMatched;
    public UnityAction<bool> OnSecondCardTap;
}