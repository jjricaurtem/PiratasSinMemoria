using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "GameEventChannel", menuName = "MemorylessPirates/Events/GameEventChannel")]
public class GameEventChannel : ScriptableObject
{
    public UnityAction<bool> OnPauseEvent;
    public UnityAction<bool> OnGameEnd;

    public void GamePause(bool isPause) => OnPauseEvent?.Invoke(isPause);

    public void GameEnd(bool isAWin) => OnGameEnd?.Invoke(isAWin);
}