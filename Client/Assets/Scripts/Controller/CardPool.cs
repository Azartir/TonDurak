using UnityEngine;
using System.Collections.Generic;

public class CardPool : MonoBehaviour
{
    public GameObject[] cardPrefabs; // Массив префабов карт
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    private Canvas canvas; // Ссылка на Canvas для установки родителя

    private void Start()
    {
        canvas = FindObjectOfType<Canvas>(); // Находим Canvas в сцене
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
                // Заполняем пул префабами
                GameObject newCard = Instantiate(prefab);
                newCard.SetActive(false);
                newCard.transform.SetParent(canvas.transform); // Устанавливаем Canvas в качестве родителя
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
        card.transform.SetParent(canvas.transform); // Возвращаем карту в пул Canvas

        if (!poolDictionary.ContainsKey(cardName))
        {
            poolDictionary[cardName] = new Queue<GameObject>();
        }

        poolDictionary[cardName].Enqueue(card);
    }

    private void LogKeys<TKey, TValue>(Dictionary<TKey, TValue> dictionary)
    {
        // Собираем все ключи в строку
        string keys = "Dictionary Keys:\n";

        foreach (TKey key in dictionary.Keys)
        {
            keys += key.ToString() + "\n";
        }

        // Выводим ключи в лог
        Debug.Log(keys);
    }
}
