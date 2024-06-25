using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using Unity.VisualScripting;

public class CardDragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform cardRectTransform;
    private CanvasGroup canvasGroup;
    private Vector3 originalPosition; // Исходная позиция в начале перетаскивания
    private Transform originalParent; // Исходный родительский объект в начале перетаскивания
    private Canvas canvas;
    private bool isDragging;

    private void Start()
    {
        cardRectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        canvas = GetComponentInParent<Canvas>();
        if (canvas == null)
        {
            Debug.LogError("Canvas не найден на сцене. Убедитесь, что у вас есть Canvas в иерархии.");
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = cardRectTransform.position; // Запоминаем позицию в момент начала перетаскивания
        originalParent = transform.parent; // Запоминаем родителя в момент начала перетаскивания
        canvasGroup.blocksRaycasts = false; // Отключаем Raycast, чтобы карта не блокировала события
        isDragging = true; // Устанавливаем флаг перетаскивания
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
        canvasGroup.blocksRaycasts = true; // Включаем Raycast
        isDragging = false; // Сбрасываем флаг перетаскивания

        // Проверка на наличие валидного объекта в результате Raycast
        if (eventData.pointerCurrentRaycast.gameObject != null)
        {
            GameObject hitObject = eventData.pointerCurrentRaycast.gameObject;

            // Проверяем, попали ли мы в другой объект с тегом "Card"
            if (hitObject.CompareTag("Card"))
            {
                // Перемещаем текущую карту на позицию другой карты
                PlaceCardOnTop(hitObject);
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

    private void ResetCardPosition()
    {
        // Возвращаем карту на позицию, с которой её начали перетаскивать
        cardRectTransform.DOMove(originalPosition, 0.3f).OnComplete(() =>
        {
            transform.SetParent(originalParent, false); // Возвращаем карту в исходного родителя
            transform.localPosition = Vector3.zero; // Сбрасываем локальную позицию относительно родителя
            transform.localRotation = Quaternion.identity; // Сбрасываем локальный поворот
        });
    }

    private void PlaceCardOnTop(GameObject targetCard)
    {
        // Устанавливаем текущую карту поверх другой карты
        transform.SetParent(targetCard.transform.parent, true);
        transform.SetAsLastSibling(); // Перемещаем текущую карту на передний план (последний элемент в иерархии)
        cardRectTransform.position = targetCard.transform.position;
        targetCard.transform.DORotate(new Vector3(0, 0, 5), 0.5f, RotateMode.WorldAxisAdd).SetEase(Ease.OutQuad);
        targetCard.transform.DOLocalMoveY(cardRectTransform.localPosition.y + 15, 0.5f).SetEase(Ease.OutQuad);
        targetCard.transform.DOLocalMoveX(cardRectTransform.localPosition.x - 25, 0.2f).SetEase(Ease.OutQuad);
        cardRectTransform.DORotate(new Vector3(0, 0, -10), 0.5f, RotateMode.WorldAxisAdd).SetEase(Ease.OutQuad);
        cardRectTransform.DOLocalMoveY(cardRectTransform.localPosition.y - 30, 0.5f).SetEase(Ease.OutQuad);
        cardRectTransform.DOLocalMoveX(cardRectTransform.localPosition.x + 25, 0.2f).SetEase(Ease.OutQuad);
        this.enabled = false;
    }

    // Обработчик, чтобы вернуть карту на место, если она не была перетащена
    private void LateUpdate()
    {
        if (!isDragging)
        {
            originalPosition = cardRectTransform.position;
        }
    }
}
