using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public interface ILobbyInfoProvider
{
    Task<int> GetPlayerCountAsync(); // ����� �������
    Task<string> GetTrumpCardAsync(); // ��� ������
    Task<int> GetCurrentTurnPlayerAsync(); // ����� ������, ������� �����
    Task<string[]> GetCurrentAvailableTurnCardAsync(); // ������ ���� � ���� ��������� ��� ����
    Task<Dictionary<int, string>> GetPlayerNameAsync(); // Dictionary<�����, ���>
    Task<Dictionary<int, int>> GetPlayerCardCountsAsync(); // Dictionary<�����, ���������� ����>
    Task<int> GetDeckCardsCountAsync(); // ���������� ���� � ������
    Task<Dictionary<int, Sprite>> GetPlayersIconAsync(); // ������ �������
}
