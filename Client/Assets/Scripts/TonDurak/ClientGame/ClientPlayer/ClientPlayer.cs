using Mirror;

namespace TonDurakClient.Game
{
    public abstract class ClientPlayer : IClientPlayer
    {
        protected bool isMe;

        public bool IsMe => isMe;
        public virtual void Serialize(NetworkWriter writer) { }
        public virtual void Deserialize(NetworkReader reader) { }
    }

    public interface IClientPlayer
    {
        bool IsMe { get; }
        void Serialize(NetworkWriter writer);
        void Deserialize(NetworkReader reader);
    }
}
