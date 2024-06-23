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
        // �������������� ������ ��� ������ ����� �� ����
        Card droppedCard = other.GetComponent<Card>();
        if (droppedCard != null)
        {
            // ������: ��������� ������� ������ ����� �� ����
            Debug.Log("Card dropped on the table: " + droppedCard.name);
            gameObject.SetActive(false);
            // ����� ����� �������� �������������� ��������, ��������� � ������
        }
    }
}
