using UnityEngine;
using UnityEngine.Events;

namespace Commons.Events
{
    [CreateAssetMenu(fileName = "TableEventChannel", menuName = "MemoryLessPirates/Events/TableEventChannel")]
    public class TableEventChannel : ScriptableObject
    {
        public UnityAction<int> OnCardHover;
        public UnityAction<bool> OnCardsInteractionActivation;
        public UnityAction<int> OnRemoveCoin;

        public void SetCardsInteractionActive(bool active) => OnCardsInteractionActivation?.Invoke(active);

        public void HoverCard(int cardId) => OnCardHover?.Invoke(cardId);
        public void RemoveCoin(int playerNumber) => OnRemoveCoin?.Invoke(playerNumber);
    }
}