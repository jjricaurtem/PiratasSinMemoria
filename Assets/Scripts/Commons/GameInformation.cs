using UnityEngine;

namespace Commons
{
    [CreateAssetMenu(fileName = "GameInformation", menuName = "MemoryLessPirates/Data/GameInformation", order = 0)]
    public class GameInformation : ScriptableObject
    {
        public int numberOfPlayers = 1;
        public int[] playerCoins = {3, 3};

        public bool IsMultiplayer() => numberOfPlayers > 1;
    }
}