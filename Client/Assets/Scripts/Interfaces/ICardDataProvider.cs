using System.Threading.Tasks;

public interface ICardDataProvider
{
    Task<string[]> GetCardDeal(); //������ ���� ��� ������
    //�� ���, �� ������ ��� �������� ����� � ����� ����
}
