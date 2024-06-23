using UnityEngine;

public class TableDropZone : MonoBehaviour
{
    // Позиция, куда карты будут сбрасываться на столе
    [SerializeField] private Transform dropPosition;

    public Vector3 GetDropPosition()
    {
        return dropPosition.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Дополнительная логика при сбросе карты на зону
        Card droppedCard = other.GetComponent<Card>();
        if (droppedCard != null)
        {
            // Пример: обработка события сброса карты на стол
            Debug.Log("Card dropped on the table: " + droppedCard.name);
            gameObject.SetActive(false);
            // Здесь можно добавить дополнительные действия, связанные с картой
        }
    }
}
