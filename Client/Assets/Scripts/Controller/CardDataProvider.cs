using UnityEngine;

public class CardDataProvider : MonoBehaviour, ICardDataProvider
{
    // ����� �� ������ ������ ���������� ����� ���� ��� �������
    private SimpleCard[] cardDeal = {
        new SimpleCard(Rank.Jack, Suit.Hearts),
        new SimpleCard(Rank.Queen, Suit.Hearts),
        new SimpleCard(Rank.King, Suit.Hearts)
        // �������� ������ ���� �� �������������
    };

    public SimpleCard[] GetCardDeal()
    {
        return cardDeal;
    }
}
