using System.Collections.Generic;

namespace TonDurakServer.Game
{
    public abstract class ServerCardPlayer : ServerPlayer
    {
        protected ICollection<Card> hand;

        protected ServerCardPlayer(int id) : base(id)
        {
        }

        public int CardsCount => hand.Count;

        public bool ContainsCard(Suit suit, Rank rank, out Card card)
        {
            foreach (var handCard in hand)
                if (handCard.Suit == suit && handCard.Rank == rank)
                {
                    card = handCard;
                    return true;
                }

            card = null;
            return false;
        }
    }

    public interface ICardPlayer : IPlayer
    {
        int CardsCount { get; }
        void AddCard(Card card);
        void RemoveCard(Card card);
        bool ContainsCard(Suit suit, Rank rank, out Card card);
    }
}