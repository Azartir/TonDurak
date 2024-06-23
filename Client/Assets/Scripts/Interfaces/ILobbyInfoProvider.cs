using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public interface ILobbyInfoProvider
{
    Task<int> GetPlayerCountAsync(); // число игроков
    Task<string> GetTrumpCardAsync(); // Имя козыря
    Task<int> GetCurrentTurnPlayerAsync(); // номер игрока, который ходит
    Task<string[]> GetCurrentAvailableTurnCardAsync(); // список карт в руке доступных для хода
    Task<Dictionary<int, string>> GetPlayerNameAsync(); // Dictionary<номер, имя>
    Task<Dictionary<int, int>> GetPlayerCardCountsAsync(); // Dictionary<номер, количество карт>
    Task<int> GetDeckCardsCountAsync(); // количество карт в колоде
    Task<Dictionary<int, Sprite>> GetPlayersIconAsync(); // Иконки игроков
}
