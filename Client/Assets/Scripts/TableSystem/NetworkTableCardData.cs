using CardsInventorySystem.CardsSystem;
using MultiplayerSystem.CustomData;

namespace TableSystem
{
    [System.Serializable]
    public struct NetworkTableCardData
    {
        public NetworkCardData CurrentCardData;
        public NetworkCardData BitCardData;

        public NetworkTableCardData(NetworkCardData currentCard, NetworkCardData bitCard)
        {
            CurrentCardData = currentCard;
            BitCardData = bitCard;
        }

        public NetworkTableCardData(NetworkCardData currentCard)
        {
            CurrentCardData = currentCard;
            BitCardData = new NetworkCardData(-1, CardType.Clubs);
        }
    }
}