using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using Unity.VisualScripting;

public class CardDragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform cardRectTransform;
    private CanvasGroup canvasGroup;
    private Vector3 originalPosition; // �������� ������� � ������ ��������������
    private Transform originalParent; // �������� ������������ ������ � ������ ��������������
    private Canvas canvas;
    private bool isDragging;

    private void Start()
    {
        cardRectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        canvas = GetComponentInParent<Canvas>();
        if (canvas == null)
        {
            Debug.LogError("Canvas �� ������ �� �����. ���������, ��� � ��� ���� Canvas � ��������.");
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = cardRectTransform.position; // ���������� ������� � ������ ������ ��������������
        originalParent = transform.parent; // ���������� �������� � ������ ������ ��������������
        canvasGroup.blocksRaycasts = false; // ��������� Raycast, ����� ����� �� ����������� �������
        isDragging = true; // ������������� ���� ��������������
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (canvas != null)
        {
            // ����������� ����� �� ������ � ������ �������� Canvas
            cardRectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true; // �������� Raycast
        isDragging = false; // ���������� ���� ��������������

        // �������� �� ������� ��������� ������� � ���������� Raycast
        if (eventData.pointerCurrentRaycast.gameObject != null)
        {
            GameObject hitObject = eventData.pointerCurrentRaycast.gameObject;

            // ���������, ������ �� �� � ������ ������ � ����� "Card"
            if (hitObject.CompareTag("Card"))
            {
                // ���������� ������� ����� �� ������� ������ �����
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
        // ���������� ����� �� �������, � ������� � ������ �������������
        cardRectTransform.DOMove(originalPosition, 0.3f).OnComplete(() =>
        {
            transform.SetParent(originalParent, false); // ���������� ����� � ��������� ��������
            transform.localPosition = Vector3.zero; // ���������� ��������� ������� ������������ ��������
            transform.localRotation = Quaternion.identity; // ���������� ��������� �������
        });
    }

    private void PlaceCardOnTop(GameObject targetCard)
    {
        // ������������� ������� ����� ������ ������ �����
        transform.SetParent(targetCard.transform.parent, true);
        transform.SetAsLastSibling(); // ���������� ������� ����� �� �������� ���� (��������� ������� � ��������)
        cardRectTransform.position = targetCard.transform.position;
        targetCard.transform.DORotate(new Vector3(0, 0, 5), 0.5f, RotateMode.WorldAxisAdd).SetEase(Ease.OutQuad);
        targetCard.transform.DOLocalMoveY(cardRectTransform.localPosition.y + 15, 0.5f).SetEase(Ease.OutQuad);
        targetCard.transform.DOLocalMoveX(cardRectTransform.localPosition.x - 25, 0.2f).SetEase(Ease.OutQuad);
        cardRectTransform.DORotate(new Vector3(0, 0, -10), 0.5f, RotateMode.WorldAxisAdd).SetEase(Ease.OutQuad);
        cardRectTransform.DOLocalMoveY(cardRectTransform.localPosition.y - 30, 0.5f).SetEase(Ease.OutQuad);
        cardRectTransform.DOLocalMoveX(cardRectTransform.localPosition.x + 25, 0.2f).SetEase(Ease.OutQuad);
        this.enabled = false;
    }

    // ����������, ����� ������� ����� �� �����, ���� ��� �� ���� ����������
    private void LateUpdate()
    {
        if (!isDragging)
        {
            originalPosition = cardRectTransform.position;
        }
    }
}
