using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameplaySimulation : MonoBehaviour
{
    public Transform launchPoint; // Точка, из которой будут брошены карты
    public Transform[] dropPoints; // Массив точек для размещения карт
    public CardPool cardPool;
    public Transform targetPoint; // Целевая точка для перемещения карт
    public float moveDuration = 0.5f; // Продолжительность перемещения
    public string cardPrefix = "(Clone)";
    public string[] cardCodes; // Массив строк, представляющих коды карт

    [SerializeField] private Button launchButton; // Button to launch the object
    public GameObject objectToLaunch; // Object to launch
    private void Start()
    {
        if (launchButton != null)
        {
            launchButton.onClick.AddListener(ActivateObject); 
            launchButton.onClick.AddListener(MoveActiveCardsToPoint);
        }
        StartCoroutine(SimulateGameplay());
    }

    private IEnumerator SimulateGameplay()
    {
        yield return SimulatePlayersDroppingCards();
    }

    private IEnumerator SimulatePlayersDroppingCards()
    {
        if (dropPoints.Length == 0)
        {
            Debug.LogError("Table points are not set or empty.");
            yield break;
        }

        if (cardCodes == null || cardCodes.Length == 0)
        {
            Debug.LogError("No card codes provided.");
            yield break;
        }

        // Simulate dropping cards to table drop points
        for (int i = 0; i < dropPoints.Length; i++)
        {
            // Check if index is within bounds of cardCodes array
            if (i < cardCodes.Length)
            {
                string cardCode = cardCodes[i];
                GameObject cardObject = cardPool.GetCard(cardCode); // Получаем объект карты из CardPool по коду

                if (cardObject == null)
                {
                    Debug.LogWarning($"Failed to create card object for prefab with code {cardCode}");
                    continue;
                }

                // Устанавливаем начальную позицию карты в точке запуска (launchPoint)
                cardObject.transform.position = launchPoint.position;

                Vector3 targetPosition = dropPoints[i].position;

                yield return cardObject.transform.DOMove(targetPosition, 0.5f).SetEase(Ease.OutQuad).WaitForCompletion();

                // Добавляем компонент BoxCollider2D и устанавливаем его как триггер
                BoxCollider2D collider = cardObject.GetComponent<BoxCollider2D>();
                if (collider == null)
                {
                    collider = cardObject.AddComponent<BoxCollider2D>();
                    collider.isTrigger = true;
                }

                // Устанавливаем родительский объект карты
                cardObject.transform.SetParent(dropPoints[i], true);

                // Устанавливаем позицию карты относительно нового родителя
                cardObject.transform.localPosition = Vector3.zero;
                cardObject.transform.localRotation = Quaternion.identity; // Сбрасываем локальный поворот
            }
            else
            {
                Debug.LogWarning($"Not enough card codes provided for all drop points.");
                break;
            }
        }
    }
    private void ActivateObject()
    {
        if (objectToLaunch != null)
        {
            objectToLaunch.SetActive(true);
        }
    }

    private void MoveActiveCardsToPoint()
    {
        // Получаем все активные карты
        GameObject[] activeCards = GetActiveCards();

        if (activeCards == null || activeCards.Length == 0)
        {
            Debug.LogWarning("No active cards found.");
            return;
        }

        // Перемещаем каждую карту к целевой точке
        foreach (GameObject card in activeCards)
        {
            if (card != null)
            {
                MoveCardToTarget(card, targetPoint.position);
            }
        }
    }

    public GameObject[] GetActiveCards()
    {
        List<GameObject> activeCards = new List<GameObject>();

        // Находим все активные объекты на сцене
        GameObject[] allGameObjects = FindObjectsOfType<GameObject>();

        // Проходим по всем найденным объектам
        foreach (GameObject obj in allGameObjects)
        {
            // Проверяем, что объект активен и его имя заканчивается на заданный суффикс
            if (obj.activeInHierarchy && obj.name.EndsWith(cardPrefix))
            {
                activeCards.Add(obj);
            }
        }

        return activeCards.ToArray();
    }

    private void MoveCardToTarget(GameObject card, Vector3 targetPosition)
    {
        // Используем DOTween для плавного перемещения карты к целевой точке
        card.transform.DOMove(targetPosition, moveDuration).SetEase(Ease.OutQuad);
    }
}
