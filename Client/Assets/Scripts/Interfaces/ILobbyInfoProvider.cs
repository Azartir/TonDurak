using System.Collections.Generic;

public interface ILobbyInfoProvider
{
    int GetPlayerCount(); //колво игроков
    Dictionary<int, string> GetPlayerName(); // Dictionary<айди игрока, им€>
    SimpleCard GetTrumbCard(); // козырь
    int GetPlayerMove(); // айди игрока, который ходит
    SimpleCard[] GetCurrentAvailableTurnCard(); // список карт в руке доступных дл€ хода
    SimpleCard[] GetTableCards(); //карты на столе 
    Dictionary<int, int> GetPlayerCardCounts(); // Dictionary<айди игрока, количество карт>
    int GetDeckCardsCount(); // количество карт в колоде
    Dictionary<int, byte[]> GetPlayersIcon(); // »конки игроков Dictionary<айди игрока, байт эррэй картинки>
}
