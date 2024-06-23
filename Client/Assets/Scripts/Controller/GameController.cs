using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private TestLobbyManager lobbyInfoProvider; // Ссылка на провайдер информации лобби
    [SerializeField] private CardPool cardPool;
    [Header("gameObjects")]
    [SerializeField] private GameObject[] playerObjectsMove;
    [SerializeField] private GameObject[] playerObjects; // Ссылки на объекты игроков
    [SerializeField] private TMP_Text[] playerNameTexts; // Ссылки на текстовые поля для имен игроков
    [SerializeField] private Image[] playerIcons; // Ссылки на иконки игроков
    [SerializeField] private TMP_Text[] playerCardCountTexts; // Ссылки на текстовые поля для количества карт у игроков
    [SerializeField] private Image trumCard;
    [SerializeField] private TMP_Text deckCardCountText; // Ссылка на текстовое поле для количества карт в колоде

    private void Start()
    {
        if (lobbyInfoProvider != null)
        {
            StartCoroutine(DisplayLobbyInfo());
            StartCoroutine(SetupPlayers());
        }
        else
        {
            Debug.LogError("LobbyInfoManager component is missing.");
        }
    }

    private IEnumerator DisplayLobbyInfo()
    {
        // Асинхронное получение данных
        Task<int> playerCountTask = lobbyInfoProvider.GetPlayerCountAsync();
        Task<string> trumpCardTask = lobbyInfoProvider.GetTrumpCardAsync();
        Task<int> currentTurnPlayerTask = lobbyInfoProvider.GetCurrentTurnPlayerAsync();
        Task<string[]> availableTurnCardsTask = lobbyInfoProvider.GetCurrentAvailableTurnCardAsync();
        Task<Dictionary<int, string>> playerNamesTask = lobbyInfoProvider.GetPlayerNameAsync();
        Task<Dictionary<int, int>> playerCardCountsTask = lobbyInfoProvider.GetPlayerCardCountsAsync();
        Task<int> deckCardsCountTask = lobbyInfoProvider.GetDeckCardsCountAsync();

        // Ожидание завершения всех задач
        yield return new WaitUntil(() => playerCountTask.IsCompleted && trumpCardTask.IsCompleted &&
                                        currentTurnPlayerTask.IsCompleted && availableTurnCardsTask.IsCompleted &&
                                        playerNamesTask.IsCompleted && playerCardCountsTask.IsCompleted &&
                                        deckCardsCountTask.IsCompleted);

        // Получение результатов
        int playerCount = playerCountTask.Result;
        string trumpCard = trumpCardTask.Result;
        int currentTurnPlayer = currentTurnPlayerTask.Result;
        string[] availableTurnCards = availableTurnCardsTask.Result;
        Dictionary<int, string> playerNames = playerNamesTask.Result;
        Dictionary<int, int> playerCardCounts = playerCardCountsTask.Result;
        int deckCardsCount = deckCardsCountTask.Result;

        // Отображение информации
        Debug.Log("Player Count: " + playerCount);
        Debug.Log("Trump Card: " + trumpCard);
        Debug.Log("Current Turn Player: " + currentTurnPlayer);
        Debug.Log("Available Turn Cards: " + string.Join(", ", availableTurnCards));
        Debug.Log("Deck Cards Count: " + deckCardsCount);

        foreach (var playerName in playerNames)
        {
            Debug.Log("Player " + playerName.Key + " Name: " + playerName.Value);
        }

        foreach (var playerCardCount in playerCardCounts)
        {
            Debug.Log("Player " + playerCardCount.Key + " Card Count: " + playerCardCount.Value);
        }

        // Вызываем методы-корутины для обновления интерфейса
        yield return UpdateTrumpCardImage(trumpCard);
        yield return ActivateObjectsByCurrentPlayer(currentTurnPlayer);
        yield return AddCardDragAndDropToAvailableCards(availableTurnCards);

        // Обновляем количество карт у каждого игрока
        for (int i = 0; i < playerCount; i++)
        {
            yield return UpdatePlayerCardCountText(i + 1, playerCardCounts.ContainsKey(i + 1) ? playerCardCounts[i + 1] : 0);
        }

        // Обновляем количество карт в колоде
        yield return UpdateDeckCardCountText(deckCardsCount);
    }

    private IEnumerator SetupPlayers()
    {
        // Получаем количество игроков
        Task<int> playerCountTask = lobbyInfoProvider.GetPlayerCountAsync();
        yield return new WaitUntil(() => playerCountTask.IsCompleted);
        int playerCount = playerCountTask.Result;
        Debug.Log("Player Count: " + playerCount);

        // Получаем имена игроков
        Task<Dictionary<int, string>> playerNamesTask = lobbyInfoProvider.GetPlayerNameAsync();
        yield return new WaitUntil(() => playerNamesTask.IsCompleted);
        Dictionary<int, string> playerNames = playerNamesTask.Result;

        // Получаем иконки игроков
        Task<Dictionary<int, Sprite>> playerIconsTask = lobbyInfoProvider.GetPlayersIconAsync();
        yield return new WaitUntil(() => playerIconsTask.IsCompleted);
        Dictionary<int, Sprite> icons = playerIconsTask.Result;

        // Настраиваем объекты игроков
        for (int i = 0; i < playerObjects.Length; i++)
        {
            if (i < playerCount)
            {
                playerObjects[i].SetActive(true);
                playerNameTexts[i].text = playerNames.ContainsKey(i + 1) ? playerNames[i + 1] : "Unknown Player";

                // Устанавливаем иконку игрока
                if (icons.ContainsKey(i + 1))
                {
                    playerIcons[i].sprite = icons[i + 1];
                }
                else
                {
                    Debug.LogWarning("Icon for Player " + (i + 1) + " not found.");
                }
            }
            else
            {
                playerObjects[i].SetActive(false);
            }
        }
    }

    private IEnumerator UpdateTrumpCardImage(string trumpCardName)
    {
        // Найти картинку или спрайт, на котором нужно изменить изображение
        // Предположим, что у вас есть объект, на котором расположен компонент Image
        Image imageToUpdate = playerIcons[0]; // Замените на реальный компонент Image, на котором нужно обновить изображение

        if (imageToUpdate != null)
        {
            // Загрузить спрайт по имени козыря (предполагается, что имя спрайта совпадает с именем козыря)
            Sprite newSprite = Resources.Load<Sprite>(trumpCardName); // Замените на ваш метод загрузки спрайта

            if (newSprite != null)
            {
                imageToUpdate.sprite = newSprite;
            }
            else
            {
                Debug.LogWarning("Sprite for trump card " + trumpCardName + " not found.");
            }
        }
        else
        {
            Debug.LogWarning("Image component to update not found.");
        }

        yield return null;
    }

    private IEnumerator ActivateObjectsByCurrentPlayer(int currentPlayerNumber)
    {
        // Предположим, что у вас есть массив объектов, которые нужно активировать в зависимости от номера текущего игрока
        // Используйте вашу логику активации объектов здесь
        // Например:
        for (int i = 0; i < playerObjectsMove.Length; i++)
        {
            if (i + 1 == currentPlayerNumber)
            {
                playerObjectsMove[i].SetActive(true);
            }
            else
            {
                playerObjectsMove[i].SetActive(false);
            }
        }

        yield return null;
    }

    private IEnumerator AddCardDragAndDropToAvailableCards(string[] availableCards)
    {
        foreach (string cardName in availableCards)
        {
            // Поиск объекта карты в пуле по ключу (например, по имени карты)
            GameObject cardObject = cardPool.GetCard(cardName);

            if (cardObject != null)
            {
                // Активация объекта, если он неактивен
                if (!cardObject.activeSelf)
                {
                    cardObject.SetActive(true);
                }

                // Проверка наличия компонента CardDragAndDrop
                CardDragAndDrop dragAndDropComponent = cardObject.GetComponent<CardDragAndDrop>();

                if (dragAndDropComponent == null)
                {
                    dragAndDropComponent = cardObject.AddComponent<CardDragAndDrop>();
                }
                else
                {
                    Debug.Log("Card object already has CardDragAndDrop component: " + cardName);
                }
            }
            else
            {
                // Объект карты не найден в пуле
                Debug.LogWarning("Card object not found in pool: " + cardName);
            }
        }

        yield return null;
    }

    private IEnumerator UpdatePlayerCardCountText(int playerNumber, int cardCount)
    {
        // Предположим, что у вас есть массив текстовых полей для количества карт игроков
        TMP_Text cardCountText = playerCardCountTexts[playerNumber - 1]; // Замените на ваше реальное текстовое поле

        if (cardCountText != null)
        {
            string cardText = cardCount == 1 ? "1 card" : cardCount + " cards";
            cardCountText.text = cardText;
        }
        else
        {
            Debug.LogWarning("Player " + playerNumber + " card count text not found.");
        }

        yield return null;
    }

    private IEnumerator UpdateDeckCardCountText(int deckCount)
    {
        if (deckCardCountText != null)
        {
            deckCardCountText.text = deckCount.ToString();
        }
        else
        {
            Debug.LogWarning("Deck card count text not found.");
        }

        yield return null;
    }
}