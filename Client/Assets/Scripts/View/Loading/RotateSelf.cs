using UnityEngine;
using DG.Tweening;

public class RotateSelf : MonoBehaviour
{
    public float rotateDuration = 2f; // ����������������� �������� (� ��������)
    public float rotateAngle = 360f; // ���� �������� (�� ��������� - ������ ������)

    void Start()
    {
        // �������� ����� ��� �������� �������
        RotateObject();
    }

    void RotateObject()
    {
        // ���������� DOTween ��� �������� �������� ������� ������ ����
        transform.DORotate(new Vector3(0, 0, rotateAngle), rotateDuration, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Restart) // ����������� ��������
            .SetEase(Ease.Linear); // ������ �������� ��������� (���������� ��������)
    }
}