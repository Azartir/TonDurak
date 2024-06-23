namespace TonDurakClient.Game
{
    public struct Client2ServerDurakPlayerPacket
    {
        public AttackPacket AttackPacket { get; private set; }
        public DefendPacket DefendPacket { get; private set; }
        public TakePacket TakePacket { get; private set; }
        public TossPacket TossPacket { get; private set; }
        public TransferPacket TransferPacket { get; private set; }
        public PassPacket PassPacket { get; private set; }

        public Client2ServerDurakPlayerPacket(
            AttackPacket attackPacket,
            DefendPacket defendPacket,
            TakePacket takePacket,
            TossPacket tossPacket,
            TransferPacket transferPacket,
            PassPacket passPacket)
        {
            AttackPacket = attackPacket;
            DefendPacket = defendPacket;
            TakePacket = takePacket;
            TossPacket = tossPacket;
            TransferPacket = transferPacket;
            PassPacket = passPacket;
        }
    }

    public struct AttackPacket
    {
        public bool HasCriticalData { get; set; }
        public Suit Suit { get; set; }
        public Rank Rank { get; set; }
    }

    public struct DefendPacket
    {
        public bool HasCriticalData { get; set; }
        public Suit MySuit { get; set; }
        public Rank MyRank { get; set; }
        public Suit OtherSuit { get; set; }
        public Rank OtherRank { get; set; }
    }

    public struct TakePacket
    {
        public bool HasCriticalData { get; set; }
    }

    public struct TossPacket
    {
        public bool HasCriticalData { get; set; }
        public Suit Suit { get; set; }
        public Rank Rank { get; set; }
    }

    public struct TransferPacket
    {
        public bool HasCriticalData { get; set; }
        public Suit Suit { get; set; }
        public Rank Rank { get; set; }
    }

    public struct PassPacket
    {
        public bool HasCriticalData { get; set; }
    }
}
