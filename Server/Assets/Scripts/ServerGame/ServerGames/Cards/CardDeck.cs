using System.Collections.Generic;

namespace TonDurakServer.Game
{
    public class CardDeck : ICardDeck
    {
        private IShuffleStrategy _strategy;

        protected Stack<Card> _cards;

        public CardDeck(Stack<Card> cards, IShuffleStrategy strategy)
        {
            _strategy = strategy;
            _cards = cards;
        }

        public void Shuffle()
        {
            _strategy.Shuffle(_cards);
        }

        public Card DrawCard()
        {
            return _cards.Pop();
        }

        public int GetCardsCount()
        {
            return _cards.Count;
        }
    }

    public interface ICardDeck
    {
        void Shuffle();
        Card DrawCard();
        int GetCardsCount();
    }
}
