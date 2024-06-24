
public struct SimpleCard
{
    public Rank CardRank { get; }
    public Suit CardSuit { get; }

    public SimpleCard(Rank rank, Suit suit)
    {
        CardRank = rank;
        CardSuit = suit;
    }

    // ����� ��� �������������� ����� � ������ �� ���� 6_H, J_D � �.�.
    public string ToCardCode()
    {
        return $"{RankToString(CardRank)}_{SuitToString(CardSuit)}";
    }

    // ����� ��� �������������� ����� � ������
    private static string RankToString(Rank rank)
    {
        switch (rank)
        {
            case Rank.Two: return "2";
            case Rank.Three: return "3";
            case Rank.Four: return "4";
            case Rank.Five: return "5";
            case Rank.Six: return "6";
            case Rank.Seven: return "7";
            case Rank.Eight: return "8";
            case Rank.Nine: return "9";
            case Rank.Ten: return "10";
            case Rank.Jack: return "J";
            case Rank.Queen: return "Q";
            case Rank.King: return "K";
            case Rank.Ace: return "A";
            default: return string.Empty; // ����������� ������, ����� ���� ������� ��� �������
        }
    }

    // ����� ��� �������������� ����� � ������
    private static string SuitToString(Suit suit)
    {
        switch (suit)
        {
            case Suit.Hearts: return "H";
            case Suit.Diamonds: return "D";
            case Suit.Clubs: return "C";
            case Suit.Spades: return "S";
            default: return string.Empty; // ����������� ������, ����� ���� ������� ��� �������
        }
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