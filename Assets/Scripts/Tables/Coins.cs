using Commons;
using Commons.Events;
using UnityEngine;

namespace Tables
{
    public class Coins : MonoBehaviour
    {
        private static readonly int RemoveCoinHash = Animator.StringToHash("RemoveCoin");
        [SerializeField] private TableEventChannel tableEventChannel;
        [SerializeField] private GameEventChannel gameEventChannel;
        [SerializeField] private GameInformation gameInformation;
        [SerializeField] private int playerNumber;
        [SerializeField] private GameObject[] coins;
        private int _currentCoinIndex;

        private void Start()
        {
            var active = playerNumber <= gameInformation.numberOfPlayers;
            gameObject.SetActive(active);
            _currentCoinIndex = 0;
            gameInformation.playerCoins[playerNumber-1] = active ?  3 : 0;
        }

        private void OnEnable()
        {
            tableEventChannel.OnRemoveCoin += OnRemoveCoin;
        }

        private void OnDisable()
        {
            tableEventChannel.OnRemoveCoin -= OnRemoveCoin;
        }

        private void OnRemoveCoin(int turnPlayerNumber)
        {
            if (playerNumber != turnPlayerNumber) return;
            var haveCoinLeft = RemoveCoin();
            if (!haveCoinLeft)
                gameEventChannel.GameEnd();
        }

        /**
         * Returns true if still has coins
         */
        private bool RemoveCoin()
        {
            if (_currentCoinIndex >= coins.Length) return false;
            gameInformation.playerCoins[playerNumber - 1]--;
            coins[_currentCoinIndex].GetComponent<Animator>().SetTrigger(RemoveCoinHash);
            _currentCoinIndex++;
            return _currentCoinIndex < coins.Length;
        }
    }
}