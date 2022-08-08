using System.Collections;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "CardEventChannel", menuName = "MemorylessPirates/Events/CardEventChannel")]
public class CardEventChannel : ScriptableObject
{
    public UnityAction<int> OnInitializationAnimationStarted;
    public UnityAction<bool> OnSecondCardTap;
    public Card currentCardUp;
    public UnityAction<CardData> OnCardTurnedUp;
    public UnityAction<string> OnMarkCardsMatched;
    public UnityAction OnCardTurnedDown;
}
