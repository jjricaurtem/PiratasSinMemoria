namespace Commons.Data
{
    public class CardData
    {
        public readonly int CardId;
        public readonly CardSo CardSo;

        public CardData(int cardId, CardSo cardSo)
        {
            CardId = cardId;
            CardSo = cardSo;
        }
    }
}