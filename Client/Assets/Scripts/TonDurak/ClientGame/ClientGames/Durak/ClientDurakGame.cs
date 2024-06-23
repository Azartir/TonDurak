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
            int playerCount = reader.ReadInt();

            if (clientPlayers.Count == 0)
                for (int i = 0; i < playerCount; i++)
                    clientPlayers.Add(new ClientDurakPlayer());

            foreach (var player in clientPlayers)
                player.Deserialize(reader);
        }
    }
}
