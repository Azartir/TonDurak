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
            Debug.LogError("Canvas �� ������ �� �����. ���������, ��� � ��� ���� Canvas � ��������.");
        }

        originalParent = transform.parent;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false; // ��������� Raycast, ����� ����� �� ����������� �������
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
        canvasGroup.blocksRaycasts = true; // �������� Raycast �������

        // �������� �� ������� ��������� ������� � ���������� Raycast
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
        // �������� ����������� ����� � ����� �������
        cardRectTransform.DOMove(dropZone.GetDropPosition(), 0.3f).OnComplete(() =>
        {
            // ������������� ������ �������� ��� �����
            transform.SetParent(dropZone.transform, false);
            transform.localPosition = Vector3.zero; // �������� ������� ������������ ������ ��������
            transform.localRotation = Quaternion.identity; // ���������� �������
            transform.DORotate(new Vector3(0, 0, 25), 2f);
        });
    }

    private void ResetCardPosition()
    {
        // ���������� ����� �� �������� �������
        cardRectTransform.DOMove(originalPosition, 0.3f).OnComplete(() =>
        {
            transform.SetParent(originalParent, false); // ���������� ��������� ��������
            transform.localPosition = Vector3.zero; // ���������� ��������� �������
            transform.localRotation = Quaternion.identity; // ���������� ��������� �������
        });
    }
}
