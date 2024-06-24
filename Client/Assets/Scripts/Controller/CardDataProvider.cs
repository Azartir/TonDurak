using UnityEngine;

public class CardDataProvider : MonoBehaviour, ICardDataProvider
{
    // Здесь вы можете задать конкретный набор карт для раздачи
    private SimpleCard[] cardDeal = {
        new SimpleCard(Rank.Jack, Suit.Hearts),
        new SimpleCard(Rank.Queen, Suit.Hearts),
        new SimpleCard(Rank.King, Suit.Hearts)
        // Добавьте больше карт по необходимости
    };

    public SimpleCard[] GetCardDeal()
    {
        return cardDeal;
    }
}
