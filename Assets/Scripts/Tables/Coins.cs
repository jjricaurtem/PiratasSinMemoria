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

        public void RemoveCoin()
        {
            if (_currentCoinIndex >= coins.Length) return;
            coins[_currentCoinIndex].GetComponent<Animator>().SetTrigger(RemoveCoinHash);
            _currentCoinIndex++;
            if (_currentCoinIndex >= coins.Length) gameEventChannel.GameEnd(false);
        }
    }
}