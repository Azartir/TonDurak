using UnityEngine;
using DG.Tweening;

public class CardDealer : MonoBehaviour
{
    [SerializeField] private CardPool cardPool;
    [SerializeField] private Transform hand; // �����, ���� ����� ����� �����
    [SerializeField] private Transform dealPoint; // �����, �� ������� ��������� �����
    [SerializeField] private float dealDuration = 0.5f; // ����� �������� ������� ����� �����
    [SerializeField] private float cardSpacing = 100f; // ���������� ����� �������
    [SerializeField] private CardDataProvider cardDataProvider;

    private void Start()
    {
        if (cardDataProvider == null)
        {
            Debug.LogError("CardDataProvider is not assigned.");
            return;
        }

        SimpleCard[] cardsToDeal = cardDataProvider.GetCardDeal();
        DealCards(cardsToDeal);
    }

    public void DealCards(SimpleCard[] cards)
    {
        if (hand == null || dealPoint == null || cardPool == null)
        {
            Debug.LogError("Hand, DealPoint, or CardPool is not assigned.");
            return;
        }

        for (int i = 0; i < cards.Length; i++)
        {
            GameObject cardObject = cardPool.GetCard(cards[i].ToCardCode());
            if (cardObject == null)
            {
                Debug.LogWarning("Card " + cards[i] + " is not available in the pool.");
                continue;
            }

            // ��������� ��������� CardDragAndDrop, ���� ��� ���
            CardDragAndDrop dragAndDropComponent = cardObject.GetComponent<CardDragAndDrop>();
            if (dragAndDropComponent == null)
            {
                dragAndDropComponent = cardObject.AddComponent<CardDragAndDrop>();
            }

            cardObject.transform.position = dealPoint.position;
            Vector3 targetPosition = hand.position + Vector3.right * cardSpacing * i;

            cardObject.transform.DOMove(targetPosition, dealDuration).SetEase(Ease.OutQuad).OnComplete(() =>
            {
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
