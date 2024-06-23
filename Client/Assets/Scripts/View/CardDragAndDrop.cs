using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class CardDragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform cardRectTransform;
    private CanvasGroup canvasGroup;
    private Vector3 originalPosition;
    private Canvas canvas;
    private Transform originalParent;

    private void Start()
    {
        cardRectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        originalPosition = cardRectTransform.position;

        canvas = GetComponentInParent<Canvas>();
        if (canvas == null)
        {
            Debug.LogError("Canvas не найден на сцене. Убедитесь, что у вас есть Canvas в иерархии.");
        }

        originalParent = transform.parent;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false; // Отключаем Raycast, чтобы карта не блокировала события
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (canvas != null)
        {
            // Перемещение карты по экрану с учетом масштаба Canvas
            cardRectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true; // Включаем Raycast обратно

        // Проверка на наличие валидного объекта в результате Raycast
        if (eventData.pointerCurrentRaycast.gameObject != null)
        {
            TableDropZone dropZone = eventData.pointerCurrentRaycast.gameObject.GetComponent<TableDropZone>();
            if (dropZone != null)
            {
                MoveCardToDropZone(dropZone);
            }
            else
            {
                ResetCardPosition();
            }
        }
        else
        {
            ResetCardPosition();
        }
    }

    private void MoveCardToDropZone(TableDropZone dropZone)
    {
        // Анимация перемещения карты в новую позицию
        cardRectTransform.DOMove(dropZone.GetDropPosition(), 0.3f).OnComplete(() =>
        {
            // Устанавливаем нового родителя для карты
            transform.SetParent(dropZone.transform, false);
            transform.localPosition = Vector3.zero; // Обнуляем позицию относительно нового родителя
            transform.localRotation = Quaternion.identity; // Сбрасываем поворот
            transform.DORotate(new Vector3(0, 0, 25), 2f);
        });
    }

    private void ResetCardPosition()
    {
        // Возвращаем карту на исходную позицию
        cardRectTransform.DOMove(originalPosition, 0.3f).OnComplete(() =>
        {
            transform.SetParent(originalParent, false); // Возвращаем исходного родителя
            transform.localPosition = Vector3.zero; // Сбрасываем локальную позицию
            transform.localRotation = Quaternion.identity; // Сбрасываем локальный поворот
        });
    }
}
