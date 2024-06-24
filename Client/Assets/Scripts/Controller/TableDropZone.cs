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
        // Попробуем получить компонент Card из другого объекта
        CardComponent droppedCardComponent = other.GetComponent<CardComponent>();

        if (droppedCardComponent != null)
        {
            // Получаем данные карты из компонента
            SimpleCard droppedCard = droppedCardComponent.cardData;

            // Пример: обработка события сброса карты на стол
            Debug.Log("Card dropped on the table: " + droppedCard.ToString());

            // Отключаем объект зоны сброса после сброса карты
            gameObject.SetActive(false);

            // Здесь можно добавить дополнительные действия, связанные с картой
        }
        else
        {
            // Если компонент CardComponent не найден, выводим предупреждение
            Debug.LogWarning("CardComponent not found on collider: " + other.name);
        }
    }
}

