using UnityEngine;
using System.Collections.Generic;

public class CardPool : MonoBehaviour
{
    public GameObject[] cardPrefabs; // ������ �������� ����
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    private Canvas canvas; // ������ �� Canvas ��� ��������� ��������

    private void Start()
    {
        canvas = FindObjectOfType<Canvas>(); // ������� Canvas � �����
        if (canvas == null)
        {
            Debug.LogError("Canvas �� ������ �� �����. ���������, ��� � ��� ���� Canvas � ��������.");
            return;
        }

        InitializePool();
        LogKeys(poolDictionary);
    }

    private void InitializePool()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (GameObject prefab in cardPrefabs)
        {
            string key = prefab.name;
            if (!poolDictionary.ContainsKey(key))
            {
                poolDictionary[key] = new Queue<GameObject>();
                // ��������� ��� ���������
                GameObject newCard = Instantiate(prefab);
                newCard.SetActive(false);
                newCard.transform.SetParent(canvas.transform); // ������������� Canvas � �������� ��������
                poolDictionary[key].Enqueue(newCard);
            }
        }
    }

    public GameObject GetCard(string cardName)
    {
        if (string.IsNullOrEmpty(cardName))
        {
            Debug.LogError("Card name is null or empty.");
            return null;
        }

        if (poolDictionary.ContainsKey(cardName) && poolDictionary[cardName].Count > 0)
        {
            GameObject obj = poolDictionary[cardName].Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            Debug.LogWarning("No available cards in the pool for: " + cardName);
            return null;
        }
    }

    public void ReturnCard(GameObject card, string cardName)
    {
        if (card == null)
        {
            Debug.LogError("Returned card is null.");
            return;
        }

        if (string.IsNullOrEmpty(cardName))
        {
            Debug.LogError("Card name is null or empty.");
            return;
        }

        card.SetActive(false);
        card.transform.SetParent(canvas.transform); // ���������� ����� � ��� Canvas

        if (!poolDictionary.ContainsKey(cardName))
        {
            poolDictionary[cardName] = new Queue<GameObject>();
        }

        poolDictionary[cardName].Enqueue(card);
    }

    private void LogKeys<TKey, TValue>(Dictionary<TKey, TValue> dictionary)
    {
        // �������� ��� ����� � ������
        string keys = "Dictionary Keys:\n";

        foreach (TKey key in dictionary.Keys)
        {
            keys += key.ToString() + "\n";
        }

        // ������� ����� � ���
        Debug.Log(keys);
    }
}
