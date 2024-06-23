using Mirror;

namespace TonDurakClient.Game
{
    public class ClientDurakGame : ClientCardGame
    {
        public override void Serialize(NetworkWriter writer)
        {
            foreach (var player in clientPlayers)
                player.Serialize(writer);
        }

        public override void Deserialize(NetworkReader reader)
        {
            foreach (var player in clientPlayers)
                player.Deserialize(reader);
        }
    }
}
