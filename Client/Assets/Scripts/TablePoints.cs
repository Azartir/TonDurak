using UnityEngine;

public class TablePoints : MonoBehaviour
{
    public Transform[] dropPoints; // Массив точек для размещения карт

    public void AddDropPoint(Transform newDropPoint)
    {
        if (newDropPoint == null)
        {
            Debug.LogWarning("Cannot add null drop point.");
            return;
        }

        // Увеличим массив на один элемент
        int currentLength = dropPoints.Length;
        Transform[] newDropPoints = new Transform[currentLength + 1];

        // Скопируем существующие точки в новый массив
        for (int i = 0; i < currentLength; i++)
        {
            newDropPoints[i] = dropPoints[i];
        }

        // Добавим новую точку в конец массива
        newDropPoints[currentLength] = newDropPoint;

        // Обновим ссылку на массив dropPoints
        dropPoints = newDropPoints;

        Debug.Log("Drop point added: " + newDropPoint.name);
    }
}
