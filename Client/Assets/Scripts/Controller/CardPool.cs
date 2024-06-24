using UnityEngine;
using System.Collections.Generic;

public class CardPool : MonoBehaviour
{
    public GameObject[] cardPrefabs;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    private Canvas canvas;

    private void Start()
    {
        canvas = FindObjectOfType<Canvas>();
        if (canvas == null)
        {
            Debug.LogError("Canvas не найден на сцене. Убедитесь, что у вас есть Canvas в иерархии.");
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
                GameObject newCard = Instantiate(prefab, new Vector3(-2000f, -2000f, 0f), Quaternion.identity);
                newCard.SetActive(false);
                newCard.transform.SetParent(canvas.transform);
                poolDictionary[key].Enqueue(newCard);
            }
        }
    }

    public GameObject GetCard(string cardCode)
    {
        if (string.IsNullOrEmpty(cardCode))
        {
            Debug.LogError("Card code is null or empty.");
            return null;
        }

        if (poolDictionary.ContainsKey(cardCode) && poolDictionary[cardCode].Count > 0)
        {
            GameObject obj = poolDictionary[cardCode].Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            Debug.LogWarning("No available cards in the pool for: " + cardCode);
            return null;
        }
    }

    public void ReturnCard(GameObject card, string cardCode)
    {
        if (card == null)
        {
            Debug.LogError("Returned card is null.");
            return;
        }

        if (string.IsNullOrEmpty(cardCode))
        {
            Debug.LogError("Card code is null or empty.");
            return;
        }

        card.SetActive(false);
        card.transform.SetParent(canvas.transform);

        if (!poolDictionary.ContainsKey(cardCode))
        {
            poolDictionary[cardCode] = new Queue<GameObject>();
        }

        poolDictionary[cardCode].Enqueue(card);
    }
    public GameObject[] GetAllCards()
    {
        List<GameObject> allCards = new List<GameObject>();

        foreach (var queue in poolDictionary.Values)
        {
            foreach (var card in queue)
            {
                if (card.activeSelf)
                {
                    allCards.Add(card);
                }
            }
        }

        return allCards.ToArray();
    }
    public string GetRandomCard()
    {
        if (cardPrefabs == null || cardPrefabs.Length == 0)
        {
            Debug.LogWarning("Card prefabs array is empty or null.");
            return "";
        }

        int randomIndex = Random.Range(0, cardPrefabs.Length);
        GameObject randomPrefab = cardPrefabs[randomIndex];

        string cardCode = randomPrefab.name;

        return cardCode;
    }
    private void LogKeys<TKey, TValue>(Dictionary<TKey, TValue> dictionary)
    {
        string keys = "Dictionary Keys:\n";

        foreach (TKey key in dictionary.Keys)
        {
            keys += key.ToString() + "\n";
        }

        Debug.Log(keys);
    }
}
