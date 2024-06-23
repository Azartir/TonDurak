namespace TonDurakServer.Game
{
    public struct Server2ClientDurakPlayerPacket
    {
        
    }

    public struct SelfData
    {
        public int Id;
        public int CardsCount;
        public DurakActivity Activity;
    }

    public struct ObserverData
    {
        public int Id;
        public int CardsCount;
        public DurakActivity Activity;
    }
}
