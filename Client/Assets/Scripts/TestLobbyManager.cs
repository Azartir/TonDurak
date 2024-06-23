using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class TestLobbyManager : MonoBehaviour, ILobbyInfoProvider
{
    // ѕримерные данные дл€ симул€ции
    [SerializeField] private Sprite defaultIcon;
    [SerializeField] private Sprite player1Icon;
    [SerializeField] private Sprite player2Icon;
    [SerializeField] private Sprite player3Icon;

    private int playerCount = 3;
    private string trumpCard = "A_H";
    private int currentTurnPlayer = 2;
    private string[] currentAvailableTurnCards = new string[] { "6_H" };

    private Dictionary<int, string> playerNames = new Dictionary<int, string>
    {
        { 1, "Player1" },
        { 2, "Player2" },
        { 3, "Player3" },
        { 4, "Player4" }
    };

    private Dictionary<int, int> playerCardCounts = new Dictionary<int, int>
    {
        { 1, 6 },
        { 2, 5 },
        { 3, 7 },
        { 4, 4 }
    };

    private Dictionary<int, Sprite> playersIcon = new Dictionary<int, Sprite>();

    private int deckCardsCount = 24;


    // –еализаци€ методов интерфейса
    public async Task<int> GetPlayerCountAsync()
    {
        await SimulateNetworkDelay();
        return playerCount;
    }

    public async Task<string> GetTrumpCardAsync()
    {
        await SimulateNetworkDelay();
        return trumpCard;
    }

    public async Task<int> GetCurrentTurnPlayerAsync()
    {
        await SimulateNetworkDelay();
        return currentTurnPlayer;
    }

    public async Task<string[]> GetCurrentAvailableTurnCardAsync()
    {
        await SimulateNetworkDelay();
        return currentAvailableTurnCards;
    }

    public async Task<Dictionary<int, string>> GetPlayerNameAsync()
    {
        await SimulateNetworkDelay();
        return playerNames;
    }

    public async Task<Dictionary<int, int>> GetPlayerCardCountsAsync()
    {
        await SimulateNetworkDelay();
        return playerCardCounts;
    }

    public async Task<int> GetDeckCardsCountAsync()
    {
        await SimulateNetworkDelay();
        return deckCardsCount;
    }

    public async Task<Dictionary<int, Sprite>> GetPlayersIconAsync()
    {
        await SimulateNetworkDelay();
        return playersIcon;
    }

    // ћетод дл€ симул€ции задержки сети
    private async Task SimulateNetworkDelay()
    {
        await Task.Delay(100);
    }
}
