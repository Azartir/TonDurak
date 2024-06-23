using UnityEngine;

public class GameState : MonoBehaviour
{
    public int currentRound;
    public int currentPlayerTurnIndex;
    public Player[] players;
    public Deck deck;

    public void InitializeGame(Player[] allPlayers, Deck gameDeck)
    {
        players = allPlayers;
        deck = gameDeck;
        currentRound = 1;
        currentPlayerTurnIndex = 0;

        // Deal initial cards to players
        foreach (Player player in players)
        {
            for (int i = 0; i < 6; i++) // Deal 6 cards initially
            {
                Card drawnCard = deck.DrawCard();
                if (drawnCard != null)
                    player.AddCardToHand(drawnCard);
            }
        }
    }

    public void StartNextRound()
    {
        currentRound++;
        // Implement logic to reset round-specific variables or state
    }

    public Player GetCurrentPlayer()
    {
        return players[currentPlayerTurnIndex];
    }
}
