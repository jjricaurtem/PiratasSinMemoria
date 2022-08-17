using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    [SerializeField] private CardEventChannel cardEventChannel;
    [SerializeField] private GameEventChannel gameEventChannel;
    [SerializeField] private Animator[] coinsAnimators;
    private int _currentCoinIndex = 0;
    void Start()
    {
        cardEventChannel.OnCardTurnedDown += OnCardTurnedDown;
        _currentCoinIndex = 0;
    }

    private void OnCardTurnedDown()
    {
        if (_currentCoinIndex >= coinsAnimators.Length) return;
        coinsAnimators[_currentCoinIndex].SetTrigger("RemoveCoin");
        _currentCoinIndex++;
        if (_currentCoinIndex >= coinsAnimators.Length) gameEventChannel.GameEnd(false);
    }
}
