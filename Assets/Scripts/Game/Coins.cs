using Commons.Events;
using UnityEngine;

namespace Game
{
    public class Coins : MonoBehaviour
    {
        [SerializeField] private CardEventChannel cardEventChannel;
        [SerializeField] private GameEventChannel gameEventChannel;
        [SerializeField] private GameObject[] coins;
        private int _currentCoinIndex;
        private static readonly int RemoveCoin = Animator.StringToHash("RemoveCoin");

        void Start()
        {
            _currentCoinIndex = 0;
        }

        private void OnEnable() => cardEventChannel.OnCardTurnedDown += OnCardTurnedDown;
        private void OnDisable() => cardEventChannel.OnCardTurnedDown -= OnCardTurnedDown;

        private void OnCardTurnedDown()
        {
            if (_currentCoinIndex >= coins.Length) return;
            coins[_currentCoinIndex].GetComponent<Animator>().SetTrigger(RemoveCoin);
            _currentCoinIndex++;
            if (_currentCoinIndex >= coins.Length) gameEventChannel.GameEnd(false);
        }
    }
}
