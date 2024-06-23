using Mirror;
using System;
using System.Collections.Generic;

namespace TonDurakServer.Game
{
    public abstract class ServerGame : IServerGame
    {
        protected List<ServerPlayer> serverPlayers = new List<ServerPlayer>();
        public List<ServerPlayer> ServerPlayers => serverPlayers;

        public int PlayersCount => serverPlayers.Count;

        public virtual event Action OnGameStartedEvent;
        public virtual event Action OnGameEndedEvent;
        public virtual event Action<Exception> OnGameErrorEvent;

        public GameStatus Status { get; protected set; } = GameStatus.Prepare;

        public abstract void AddPlayer(int id);
        public abstract void RemovePlayer(int id);

        public virtual void StartGame() { }

        public virtual void Tick()
        {
            foreach (var player in serverPlayers)
                player.Proceed();
        }

        public virtual void StopGame()
        {

        }

        public void ApplyPacket(int id, NetworkReader reader)
        {
            foreach (var player in serverPlayers)
                if (player.Id == id)
                {
                    player.Deserialize(reader);
                    break;
                }
        }

        public virtual void PreparePlayerPacket(int id, NetworkWriter writer) { }
    }

    public interface IServerGame
    {
        int PlayersCount { get; }
        List<ServerPlayer> ServerPlayers { get; }
        void StartGame();
        void Tick();
        void StopGame();
        void AddPlayer(int id);
        void RemovePlayer(int id);
        void PreparePlayerPacket(int id, NetworkWriter writer);
        void ApplyPacket(int id, NetworkReader reader);
        GameStatus Status { get; }
        event Action OnGameStartedEvent;
        event Action OnGameEndedEvent;
        event Action<Exception> OnGameErrorEvent;
    }

    public interface ISerializable
    {
        void Serialize();
        void Desirealize();
    }

    public enum GameStatus
    {
        Prepare,
        Running,
        Ended,
    }
}
