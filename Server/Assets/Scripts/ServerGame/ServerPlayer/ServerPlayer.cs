using Mirror;

namespace TonDurakServer.Game
{
    public abstract class ServerPlayer : IPlayer
    {
        protected int id;
        public int Id => id;

        public ServerPlayer(int id) => this.id = id;

        public virtual void Proceed() { }
        public virtual void Serialize(int id, NetworkWriter networkWriter) { }
        public virtual void Deserialize(NetworkReader reader) { }
    }

    public interface IPlayer
    {
        int Id { get; }
        void Proceed();
        public void Serialize(int id, NetworkWriter networkWriter);
        public void Deserialize(NetworkReader reader);
    }
}
