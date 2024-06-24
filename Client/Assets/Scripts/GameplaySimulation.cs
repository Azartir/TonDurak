using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameplaySimulation : MonoBehaviour
{
    public Transform launchPoint; // �����, �� ������� ����� ������� �����
    public Transform[] dropPoints; // ������ ����� ��� ���������� ����
    public CardPool cardPool;
    public Transform targetPoint; // ������� ����� ��� ����������� ����
    public float moveDuration = 0.5f; // ����������������� �����������
    public string cardPrefix = "(Clone)";
    public string[] cardCodes; // ������ �����, �������������� ���� ����

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
                GameObject cardObject = cardPool.GetCard(cardCode); // �������� ������ ����� �� CardPool �� ����

                if (cardObject == null)
                {
                    Debug.LogWarning($"Failed to create card object for prefab with code {cardCode}");
                    continue;
                }

                // ������������� ��������� ������� ����� � ����� ������� (launchPoint)
                cardObject.transform.position = launchPoint.position;

                Vector3 targetPosition = dropPoints[i].position;

                yield return cardObject.transform.DOMove(targetPosition, 0.5f).SetEase(Ease.OutQuad).WaitForCompletion();

                // ��������� ��������� BoxCollider2D � ������������� ��� ��� �������
                BoxCollider2D collider = cardObject.GetComponent<BoxCollider2D>();
                if (collider == null)
                {
                    collider = cardObject.AddComponent<BoxCollider2D>();
                    collider.isTrigger = true;
                }

                // ������������� ������������ ������ �����
                cardObject.transform.SetParent(dropPoints[i], true);

                // ������������� ������� ����� ������������ ������ ��������
                cardObject.transform.localPosition = Vector3.zero;
                cardObject.transform.localRotation = Quaternion.identity; // ���������� ��������� �������
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
        // �������� ��� �������� �����
        GameObject[] activeCards = GetActiveCards();

        if (activeCards == null || activeCards.Length == 0)
        {
            Debug.LogWarning("No active cards found.");
            return;
        }

        // ���������� ������ ����� � ������� �����
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

        // ������� ��� �������� ������� �� �����
        GameObject[] allGameObjects = FindObjectsOfType<GameObject>();

        // �������� �� ���� ��������� ��������
        foreach (GameObject obj in allGameObjects)
        {
            // ���������, ��� ������ ������� � ��� ��� ������������� �� �������� �������
            if (obj.activeInHierarchy && obj.name.EndsWith(cardPrefix))
            {
                activeCards.Add(obj);
            }
        }

        return activeCards.ToArray();
    }

    private void MoveCardToTarget(GameObject card, Vector3 targetPosition)
    {
        // ���������� DOTween ��� �������� ����������� ����� � ������� �����
        card.transform.DOMove(targetPosition, moveDuration).SetEase(Ease.OutQuad);
    }
}
