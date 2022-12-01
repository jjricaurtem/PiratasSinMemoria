using Commons.Events;
using UnityEngine;

namespace Tables
{
    public class Coins : MonoBehaviour
    {
        private static readonly int RemoveCoinHash = Animator.StringToHash("RemoveCoin");
        [SerializeField] private TableEventChannel tableEventChannel;
        [SerializeField] private GameEventChannel gameEventChannel;
        [SerializeField] private GameObject[] coins;
        private int _currentCoinIndex;

        private void Start()
        {
            _currentCoinIndex = 0;
        }

        /**
         * Returns true if still has coins
         */
        public bool RemoveCoin()
        {
            if (_currentCoinIndex >= coins.Length) return false;
            coins[_currentCoinIndex].GetComponent<Animator>().SetTrigger(RemoveCoinHash);
            _currentCoinIndex++;
            return _currentCoinIndex < coins.Length;
        }
    }
}