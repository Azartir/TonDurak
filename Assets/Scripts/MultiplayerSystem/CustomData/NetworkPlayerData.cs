namespace MultiplayerSystem.CustomData
{
    [System.Serializable]
    public struct NetworkPlayerData
    {
        public int Id;
        public ushort QueueIndex;

        public NetworkPlayerData(int id, ushort queueIndex)
        {
            Id = id;
            QueueIndex = queueIndex;
        }
    }
}