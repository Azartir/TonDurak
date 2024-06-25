using UnityEngine;
using DG.Tweening;
using System.Collections;

public class CardDealer : MonoBehaviour
{
    [SerializeField] private CardPool cardPool;
    [SerializeField] private Transform hand; // Точка, куда игрок берет карты
    [SerializeField] private Transform dealPoint; // Точка, из которой раздаются карты
    [SerializeField] private float dealDuration = 0.5f; // Время анимации раздачи одной карты
    [SerializeField] private float cardSpacing = 100f; // Расстояние между картами
    [SerializeField] private CardDataProvider cardDataProvider;

    private void Start()
    {
        if (cardDataProvider == null)
        {
            Debug.LogError("CardDataProvider is not assigned.");
            return;
        }

        SimpleCard[] cardsToDeal = cardDataProvider.GetCardDeal();
        StartDealing(cardsToDeal);
    }

    public void StartDealing(SimpleCard[] cards)
    {
        StartCoroutine(DealCards(cards));
    }

    private IEnumerator DealCards(SimpleCard[] cards)
    {
        if (hand == null || dealPoint == null || cardPool == null)
        {
            Debug.LogError("Hand, DealPoint, or CardPool is not assigned.");
            yield break; // Останавливаем выполнение, если не все параметры назначены
        }

        for (int i = 0; i < cards.Length; i++)
        {
            yield return new WaitForSeconds(1); // Задержка перед выдачей каждой карты

            GameObject cardObject = cardPool.GetCard(cards[i].ToCardCode());
            if (cardObject == null)
            {
                Debug.LogWarning($"Card {cards[i]} is not available in the pool.");
                continue;
            }

            // Добавляем компонент CardDragAndDrop, если его нет
            CardDragAndDrop dragAndDropComponent = cardObject.GetComponent<CardDragAndDrop>();
            if (dragAndDropComponent == null)
            {
                dragAndDropComponent = cardObject.AddComponent<CardDragAndDrop>();
            }

            // Устанавливаем начальную позицию карты
            cardObject.transform.position = dealPoint.position;

            // Рассчитываем целевую позицию
            Vector3 targetPosition = hand.position + Vector3.right * cardSpacing * i;

            // Перемещаем карту
            cardObject.transform.DOMove(targetPosition, dealDuration)
                .SetEase(Ease.OutQuad)
                .OnComplete(() =>
                {
                    // Устанавливаем родителя и устраиваем карты в линии
                    cardObject.transform.SetParent(hand, false);
                    ArrangeCardsInLine();
                });
        }
    }

    private void ArrangeCardsInLine()
    {
        int cardCount = hand.childCount;

        for (int i = 0; i < cardCount; i++)
        {
            Transform card = hand.GetChild(i);
            Vector3 newPosition = Vector3.right * cardSpacing * i;

            card.localPosition = newPosition;
            card.localRotation = Quaternion.identity;
        }
    }
}
