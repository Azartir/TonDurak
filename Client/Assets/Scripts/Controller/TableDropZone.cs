using UnityEngine;

public class TableDropZone : MonoBehaviour
{
    // �������, ���� ����� ����� ������������ �� �����
    [SerializeField] private Transform dropPosition;

    public Vector3 GetDropPosition()
    {
        return dropPosition.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        // ��������� �������� ��������� Card �� ������� �������
        CardComponent droppedCardComponent = other.GetComponent<CardComponent>();

        if (droppedCardComponent != null)
        {
            // �������� ������ ����� �� ����������
            SimpleCard droppedCard = droppedCardComponent.cardData;

            // ������: ��������� ������� ������ ����� �� ����
            Debug.Log("Card dropped on the table: " + droppedCard.ToString());

            // ��������� ������ ���� ������ ����� ������ �����
            gameObject.SetActive(false);

            // ����� ����� �������� �������������� ��������, ��������� � ������
        }
        else
        {
            // ���� ��������� CardComponent �� ������, ������� ��������������
            Debug.LogWarning("CardComponent not found on collider: " + other.name);
        }
    }
}

