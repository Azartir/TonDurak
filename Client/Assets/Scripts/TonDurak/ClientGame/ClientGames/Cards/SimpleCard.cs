namespace TonDurakClient.Game
{
    public struct SimpleCard
    {
        public Rank Rank { get; set; }
        public Suit Suit { get; set; }
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
