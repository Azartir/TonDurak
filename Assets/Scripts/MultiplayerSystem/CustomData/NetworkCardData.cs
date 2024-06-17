using CardsInventorySystem.CardsSystem;

namespace MultiplayerSystem.CustomData
{
    [System.Serializable]
    public struct NetworkCardData
    {
        public int Id;
        public CardType CardType;

        public NetworkCardData(int id, CardType cardType)
        {
            Id = id;
            CardType = cardType;
        }
    }
}