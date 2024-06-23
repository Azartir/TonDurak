namespace SaveSystem
{
    [System.Serializable]
    public struct RoomData
    {
        public uint Id;
        public string RoomName;
        public int PlayersCount;
        public float Bid;
        public GameType GameType;
    }
}