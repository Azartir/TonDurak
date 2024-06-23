using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    public string playerName;
    public List<Card> hand;

    public void InitializePlayer()
    {
        hand = new List<Card>();
    }

    public void AddCardToHand(Card card)
    {
        hand.Add(card);
    }

    public void RemoveCardFromHand(Card card)
    {
        hand.Remove(card);
    }
}
