using UnityEngine;

public class TablePoints : MonoBehaviour
{
    public Transform[] dropPoints; // ������ ����� ��� ���������� ����

    public void AddDropPoint(Transform newDropPoint)
    {
        if (newDropPoint == null)
        {
            Debug.LogWarning("Cannot add null drop point.");
            return;
        }

        // �������� ������ �� ���� �������
        int currentLength = dropPoints.Length;
        Transform[] newDropPoints = new Transform[currentLength + 1];

        // ��������� ������������ ����� � ����� ������
        for (int i = 0; i < currentLength; i++)
        {
            newDropPoints[i] = dropPoints[i];
        }

        // ������� ����� ����� � ����� �������
        newDropPoints[currentLength] = newDropPoint;

        // ������� ������ �� ������ dropPoints
        dropPoints = newDropPoints;

        Debug.Log("Drop point added: " + newDropPoint.name);
    }
}
