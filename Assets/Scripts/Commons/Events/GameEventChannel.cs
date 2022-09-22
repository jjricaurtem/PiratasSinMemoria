﻿using UnityEngine;
using UnityEngine.Events;

namespace Commons.Events
{
    [CreateAssetMenu(fileName = "GameEventChannel", menuName = "MemoryLessPirates/Events/GameEventChannel")]
    public class GameEventChannel : ScriptableObject
    {
        public UnityAction<bool> OnPauseEvent;
        public UnityAction<bool> OnGameEnd;

        public void GamePause(bool isPause) => OnPauseEvent?.Invoke(isPause);

        public void GameEnd(bool isAWin) => OnGameEnd?.Invoke(isAWin);
    }
}