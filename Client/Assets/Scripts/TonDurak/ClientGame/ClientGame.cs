using Mirror;
using System.Collections.Generic;
using TonDurakClient.Game;

namespace TonDurakClient
{
    public abstract class ClientGame : IClientGame
    {
        protected List<ClientPlayer> clientPlayers = new List<ClientPlayer>();
        public List<ClientPlayer> ClientPlayers => clientPlayers;

        public GameStatus Status { get; protected set; }
        public virtual void Tick() { }
        public virtual void Serialize(NetworkWriter writer) { }
        public virtual void Deserialize(NetworkReader reader) { }
    }

    public interface IClientGame
    {
        GameStatus Status { get; }
        List<ClientPlayer> ClientPlayers { get; }
        void Tick();
        void Serialize(NetworkWriter writer);
        void Deserialize(NetworkReader reader);
    }

    public enum GameStatus
    {
        Prepare,
        Running,
        Ended,
    }
}
