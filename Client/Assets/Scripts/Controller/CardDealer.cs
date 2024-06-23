using UnityEngine;
using DG.Tweening;

public class CardDealer : MonoBehaviour
{
    [SerializeField] private CardPool cardPool;
    [SerializeField] private Transform hand; // Точка, куда игрок берет карты
    [SerializeField] private Transform dealPoint; // Точка, из которой раздаются карты
    [SerializeField] private float dealDuration = 0.5f; // Время анимации раздачи одной карты
    [SerializeField] private float cardSpacing = 100f; // Расстояние между картами

    public void DealCards(string[] cardNames)
    {
        if (hand == null || dealPoint == null || cardPool == null)
        {
            Debug.LogError("Hand, DealPoint or CardPool is not assigned.");
            return;
        }

        for (int i = 0; i < cardNames.Length; i++)
        {
            GameObject card = cardPool.GetCard(cardNames[i]);
            if (card == null)
            {
                Debug.LogWarning("Card " + cardNames[i] + " is not available in the pool.");
                continue;
            }

            // Перемещаем карту к руке игрока
            card.transform.position = dealPoint.position;
            Vector3 targetPosition = hand.position + Vector3.right * cardSpacing * i;

            // Анимация перемещения карты к руке игрока
            card.transform.DOMove(targetPosition, dealDuration).SetEase(Ease.OutQuad).OnComplete(() =>
            {
                card.transform.SetParent(hand, false);
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

            // Устанавливаем позицию карты по горизонтали с учетом смещения
            card.localPosition = newPosition;
            card.localRotation = Quaternion.identity; // Сбрасываем поворот
        }
    }
}
