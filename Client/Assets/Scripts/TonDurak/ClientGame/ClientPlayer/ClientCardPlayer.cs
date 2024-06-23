using System.Collections.Generic;

namespace TonDurakClient.Game
{
    public abstract class ClientCardPlayer : ClientPlayer
    {
        protected ICollection<SimpleCard> hand = new List<SimpleCard>();
        public int CardsCount => hand.Count;
    }
}
