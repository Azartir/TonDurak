namespace TonDurakServer.Game
{
    public abstract class ServerCardsGame : ServerGame
    {
        protected ICardDeck cardDeck;

        protected abstract ICardDeck InitializeCardDeck();

        protected abstract void DealCards();
    }
}
