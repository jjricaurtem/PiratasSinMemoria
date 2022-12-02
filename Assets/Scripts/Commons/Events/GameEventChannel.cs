using UnityEngine;
using UnityEngine.Events;

namespace Commons.Events
{
    [CreateAssetMenu(fileName = "GameEventChannel", menuName = "MemoryLessPirates/Events/GameEventChannel")]
    public class GameEventChannel : ScriptableObject
    {
        public UnityAction<bool> OnPauseEvent;
        public UnityAction OnGameEnd;
        public UnityAction<int> OnTurnChange;

        public void GamePause(bool isPause) => OnPauseEvent?.Invoke(isPause);

        public void GameEnd() => OnGameEnd?.Invoke();

        public void ChangeTurn(int playerNumber) => OnTurnChange?.Invoke(playerNumber);

    }
}