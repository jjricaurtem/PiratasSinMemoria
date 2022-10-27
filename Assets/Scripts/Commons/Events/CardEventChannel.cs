using Commons.Data;
using UnityEngine;
using UnityEngine.Events;

namespace Commons.Events
{
    [CreateAssetMenu(fileName = "CardEventChannel", menuName = "MemoryLessPirates/Events/CardEventChannel")]
    public class CardEventChannel : ScriptableObject
    {
        public UnityAction<int> OnCardHover;
        public UnityAction OnCardReady;
        public UnityAction<bool> OnCardsInteractionActivation;
        public UnityAction OnCardTurnedDown;
        public UnityAction<CardData> OnCardTurnedUp;
        public UnityAction<string> OnMarkCardsMatched;

        public void CardsInteractionActive(bool active) => OnCardsInteractionActivation?.Invoke(active);

        public void CardReady() => OnCardReady?.Invoke();

        public void HoverCard(int cardId) => OnCardHover?.Invoke(cardId);
    }
}