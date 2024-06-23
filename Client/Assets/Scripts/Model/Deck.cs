using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Deck", menuName = "Card Game/Deck")]
public class Deck : ScriptableObject
{
    public List<Card> cards;

    public void InitializeDeck()
    {
        cards = new List<Card>();

        foreach (Card.Suit suit in System.Enum.GetValues(typeof(Card.Suit)))
        {
            foreach (Card.Rank rank in System.Enum.GetValues(typeof(Card.Rank)))
            {
                Card newCard = CreateCard(suit, rank);
                cards.Add(newCard);
            }
        }

        Shuffle();
    }

    private Card CreateCard(Card.Suit suit, Card.Rank rank)
    {
        Card newCard = ScriptableObject.CreateInstance<Card>();
        newCard.suit = suit;
        newCard.rank = rank;
        return newCard;
    }

    public void Shuffle()
    {
        for (int i = cards.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            Card temp = cards[i];
            cards[i] = cards[randomIndex];
            cards[randomIndex] = temp;
        }
    }

    public Card DrawCard()
    {
        if (cards.Count == 0)
            return null;

        Card drawnCard = cards[0];
        cards.RemoveAt(0);
        return drawnCard;
    }
}
