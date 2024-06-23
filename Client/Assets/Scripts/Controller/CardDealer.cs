using UnityEngine;
using DG.Tweening;

public class CardDealer : MonoBehaviour
{
    [SerializeField] private CardPool cardPool;
    [SerializeField] private Transform hand; // �����, ���� ����� ����� �����
    [SerializeField] private Transform dealPoint; // �����, �� ������� ��������� �����
    [SerializeField] private float dealDuration = 0.5f; // ����� �������� ������� ����� �����
    [SerializeField] private float cardSpacing = 100f; // ���������� ����� �������

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

            // ���������� ����� � ���� ������
            card.transform.position = dealPoint.position;
            Vector3 targetPosition = hand.position + Vector3.right * cardSpacing * i;

            // �������� ����������� ����� � ���� ������
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

            // ������������� ������� ����� �� ����������� � ������ ��������
            card.localPosition = newPosition;
            card.localRotation = Quaternion.identity; // ���������� �������
        }
    }
}
