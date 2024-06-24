public struct SimpleCard
{
    public Rank CardRank { get; }
    public Suit CardSuit { get; }

    public SimpleCard(Rank rank, Suit suit)
    {
        CardRank = rank;
        CardSuit = suit;
    }

    public override string ToString()
    {
        return $"{CardRank} of {CardSuit}";
    }
}
public enum Rank
{
    Two = 2,
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
    Ace
}
public enum Suit
{
    Hearts,
    Diamonds,
    Clubs,
    Spades
}