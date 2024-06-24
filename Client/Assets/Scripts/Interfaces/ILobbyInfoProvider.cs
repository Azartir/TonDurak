using System.Collections.Generic;

public interface ILobbyInfoProvider
{
    int GetPlayerCount(); //����� �������
    Dictionary<int, string> GetPlayerName(); // Dictionary<���� ������, ���>
    SimpleCard GetTrumbCard(); // ������
    int GetPlayerMove(); // ���� ������, ������� �����
    SimpleCard[] GetCurrentAvailableTurnCard(); // ������ ���� � ���� ��������� ��� ����
    SimpleCard[] GetTableCards(); //����� �� ����� 
    Dictionary<int, int> GetPlayerCardCounts(); // Dictionary<���� ������, ���������� ����>
    int GetDeckCardsCount(); // ���������� ���� � ������
    Dictionary<int, byte[]> GetPlayersIcon(); // ������ ������� Dictionary<���� ������, ���� ����� ��������>
}
