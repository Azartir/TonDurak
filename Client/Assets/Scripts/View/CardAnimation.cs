using UnityEngine;
using DG.Tweening;

public class CardAnimation : MonoBehaviour
{
    private Vector3 originalPosition;

    private void Start()
    {
        originalPosition = transform.position;
    }

    public void MoveCardToPosition(Vector3 targetPosition, float duration)
    {
        transform.DOMove(targetPosition, duration);
    }

    public void MoveCardToOriginalPosition(float duration)
    {
        transform.DOMove(originalPosition, duration);
    }
}
