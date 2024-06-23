namespace TonDurakServer.Game
{
    public class Card
    {
        public Suit Suit { get; protected set; }
        public Rank Rank { get; protected set; }

        public Card(Suit suit, Rank rank)
        {
            Suit = suit;
            Rank = rank;
        }

        public override string ToString()
        {
            return $"{Rank} : {Suit}";
        }
    }

    public enum Suit
    {
        Spades,
        Hearts,
        Diamonds,
        Clumbs,
    }

    public enum Rank
    {
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
        Ace,
    }
}
