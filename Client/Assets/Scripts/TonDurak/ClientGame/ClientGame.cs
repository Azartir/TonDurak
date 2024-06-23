using Mirror;
using System.Collections.Generic;
using TonDurakClient.Game;

namespace TonDurakClient
{
    public abstract class ClientGame : IClientGame
    {
        protected List<ClientPlayer> clientPlayers;

        public virtual void Serialize(NetworkWriter writer) { }
        public virtual void Deserialize(NetworkReader reader) { }
    }

    public interface IClientGame
    {
        void Serialize(NetworkWriter writer);
        void Deserialize(NetworkReader reader);
    }
}
