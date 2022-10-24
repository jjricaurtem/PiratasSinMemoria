using UnityEngine;

namespace Card
{
    [CreateAssetMenu(fileName = "NAME_CardSo", menuName = "MemoryLessPirates/Card/CardSo")]
    public class CardSo : ScriptableObject
    {
        public string cardName;
        public Sprite sprite;
    }
}