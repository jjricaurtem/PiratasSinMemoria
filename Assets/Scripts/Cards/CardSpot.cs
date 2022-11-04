using Commons.Events;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Cards
{
    public class CardSpot : MonoBehaviour
    {
        [SerializeField] private int index;
        public TableEventChannel tableEventChannel;
        public SpriteRenderer spriteRenderer;

        private void Start()
        {
            spriteRenderer.enabled = false;
        }

        private void OnEnable() =>  tableEventChannel.OnCardHover += OnCardHover;

        private void OnDisable() =>  tableEventChannel.OnCardHover -= OnCardHover;
        
        private void OnCardHover(int hoverCardId) => spriteRenderer.enabled = (hoverCardId == index);
    }
}