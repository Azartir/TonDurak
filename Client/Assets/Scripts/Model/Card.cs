using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card Game/Card")]
public class Card : ScriptableObject
{
    public enum Suit { Hearts, Diamonds, Clubs, Spades };
    public enum Rank {Two = 2, Three = 3, Four = 4, Five = 5, Six = 6, Seven = 7, Eight = 8, Nine = 9, Ten = 10, Jack = 11, Queen = 12, King = 13, Ace = 14, };

    public Suit suit;
    public Rank rank;
    public GameObject cardPrefab; // ������ ����� ��� ���������

    // ����������� ��� �������� �������� �����
    public static Card CreateCard(Suit suit, Rank rank, GameObject cardPrefab)
    {
        Card card = ScriptableObject.CreateInstance<Card>();
        card.suit = suit;
        card.rank = rank;
        card.cardPrefab = cardPrefab;
        return card;
    }
}
