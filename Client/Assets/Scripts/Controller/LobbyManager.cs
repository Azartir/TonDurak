using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoBehaviour, ILobbyInfoProvider
{
    private Dictionary<int, string> playerNames = new Dictionary<int, string>
    {
        { 1, "Igor" },
        { 2, "Muerte" },
    };

    private SimpleCard trumbCard = new SimpleCard(Rank.Ace, Suit.Hearts);

    private int playerCount = 2;

    private int playerId = 1;

    private SimpleCard[] currentAvailableTurnCards = new SimpleCard[]
    {
        new SimpleCard(Rank.Two, Suit.Clubs),
        new SimpleCard(Rank.Seven, Suit.Diamonds)
    };

    private SimpleCard[] tableCards = new SimpleCard[]
    {
        new SimpleCard(Rank.King, Suit.Spades),
        new SimpleCard(Rank.Three, Suit.Hearts)
    };

    private Dictionary<int, int> playerCardCounts = new Dictionary<int, int>
    {
        { 1, 5 },
        { 2, 2 }
    };

    private int deckCardsCount = 0;

    private Dictionary<int, byte[]> playersIcon = new Dictionary<int, byte[]>
    {
        { 1, new byte[0] }, // В реальной ситуации здесь будут данные иконок в формате byte[]
        { 2, new byte[0] }
    };

    public Dictionary<int, string> GetPlayerName()
    {
        return playerNames;
    }

    public SimpleCard GetTrumbCard()
    {
        return trumbCard;
    }

    public int GetPlayerCount()
    {
        return playerCount;
    }

    public SimpleCard[] GetCurrentAvailableTurnCard()
    {
        return currentAvailableTurnCards;
    }

    public SimpleCard[] GetTableCards()
    {
        return tableCards;
    }

    public Dictionary<int, int> GetPlayerCardCounts()
    {
        return playerCardCounts;
    }

    public int GetDeckCardsCount()
    {
        return deckCardsCount;
    }

    public Dictionary<int, byte[]> GetPlayersIcon()
    {
        return playersIcon;
    }
    public int GetPlayerMove()
    {
        return playerId;
    }
}
