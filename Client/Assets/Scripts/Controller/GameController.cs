using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private TestLobbyManager lobbyInfoProvider; // ������ �� ��������� ���������� �����
    [SerializeField] private CardPool cardPool;
    [Header("gameObjects")]
    [SerializeField] private GameObject[] playerObjectsMove;
    [SerializeField] private GameObject[] playerObjects; // ������ �� ������� �������
    [SerializeField] private TMP_Text[] playerNameTexts; // ������ �� ��������� ���� ��� ���� �������
    [SerializeField] private Image[] playerIcons; // ������ �� ������ �������
    [SerializeField] private TMP_Text[] playerCardCountTexts; // ������ �� ��������� ���� ��� ���������� ���� � �������
    [SerializeField] private Image trumCard;
    [SerializeField] private TMP_Text deckCardCountText; // ������ �� ��������� ���� ��� ���������� ���� � ������

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
        // ����������� ��������� ������
        Task<int> playerCountTask = lobbyInfoProvider.GetPlayerCountAsync();
        Task<string> trumpCardTask = lobbyInfoProvider.GetTrumpCardAsync();
        Task<int> currentTurnPlayerTask = lobbyInfoProvider.GetCurrentTurnPlayerAsync();
        Task<string[]> availableTurnCardsTask = lobbyInfoProvider.GetCurrentAvailableTurnCardAsync();
        Task<Dictionary<int, string>> playerNamesTask = lobbyInfoProvider.GetPlayerNameAsync();
        Task<Dictionary<int, int>> playerCardCountsTask = lobbyInfoProvider.GetPlayerCardCountsAsync();
        Task<int> deckCardsCountTask = lobbyInfoProvider.GetDeckCardsCountAsync();

        // �������� ���������� ���� �����
        yield return new WaitUntil(() => playerCountTask.IsCompleted && trumpCardTask.IsCompleted &&
                                        currentTurnPlayerTask.IsCompleted && availableTurnCardsTask.IsCompleted &&
                                        playerNamesTask.IsCompleted && playerCardCountsTask.IsCompleted &&
                                        deckCardsCountTask.IsCompleted);

        // ��������� �����������
        int playerCount = playerCountTask.Result;
        string trumpCard = trumpCardTask.Result;
        int currentTurnPlayer = currentTurnPlayerTask.Result;
        string[] availableTurnCards = availableTurnCardsTask.Result;
        Dictionary<int, string> playerNames = playerNamesTask.Result;
        Dictionary<int, int> playerCardCounts = playerCardCountsTask.Result;
        int deckCardsCount = deckCardsCountTask.Result;

        // ����������� ����������
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

        // �������� ������-�������� ��� ���������� ����������
        yield return UpdateTrumpCardImage(trumpCard);
        yield return ActivateObjectsByCurrentPlayer(currentTurnPlayer);
        yield return AddCardDragAndDropToAvailableCards(availableTurnCards);

        // ��������� ���������� ���� � ������� ������
        for (int i = 0; i < playerCount; i++)
        {
            yield return UpdatePlayerCardCountText(i + 1, playerCardCounts.ContainsKey(i + 1) ? playerCardCounts[i + 1] : 0);
        }

        // ��������� ���������� ���� � ������
        yield return UpdateDeckCardCountText(deckCardsCount);
    }

    private IEnumerator SetupPlayers()
    {
        // �������� ���������� �������
        Task<int> playerCountTask = lobbyInfoProvider.GetPlayerCountAsync();
        yield return new WaitUntil(() => playerCountTask.IsCompleted);
        int playerCount = playerCountTask.Result;
        Debug.Log("Player Count: " + playerCount);

        // �������� ����� �������
        Task<Dictionary<int, string>> playerNamesTask = lobbyInfoProvider.GetPlayerNameAsync();
        yield return new WaitUntil(() => playerNamesTask.IsCompleted);
        Dictionary<int, string> playerNames = playerNamesTask.Result;

        // �������� ������ �������
        Task<Dictionary<int, Sprite>> playerIconsTask = lobbyInfoProvider.GetPlayersIconAsync();
        yield return new WaitUntil(() => playerIconsTask.IsCompleted);
        Dictionary<int, Sprite> icons = playerIconsTask.Result;

        // ����������� ������� �������
        for (int i = 0; i < playerObjects.Length; i++)
        {
            if (i < playerCount)
            {
                playerObjects[i].SetActive(true);
                playerNameTexts[i].text = playerNames.ContainsKey(i + 1) ? playerNames[i + 1] : "Unknown Player";

                // ������������� ������ ������
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
        // ����� �������� ��� ������, �� ������� ����� �������� �����������
        // �����������, ��� � ��� ���� ������, �� ������� ���������� ��������� Image
        Image imageToUpdate = playerIcons[0]; // �������� �� �������� ��������� Image, �� ������� ����� �������� �����������

        if (imageToUpdate != null)
        {
            // ��������� ������ �� ����� ������ (��������������, ��� ��� ������� ��������� � ������ ������)
            Sprite newSprite = Resources.Load<Sprite>(trumpCardName); // �������� �� ��� ����� �������� �������

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
        // �����������, ��� � ��� ���� ������ ��������, ������� ����� ������������ � ����������� �� ������ �������� ������
        // ����������� ���� ������ ��������� �������� �����
        // ��������:
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
            // ����� ������� ����� � ���� �� ����� (��������, �� ����� �����)
            GameObject cardObject = cardPool.GetCard(cardName);

            if (cardObject != null)
            {
                // ��������� �������, ���� �� ���������
                if (!cardObject.activeSelf)
                {
                    cardObject.SetActive(true);
                }

                // �������� ������� ���������� CardDragAndDrop
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
                // ������ ����� �� ������ � ����
                Debug.LogWarning("Card object not found in pool: " + cardName);
            }
        }

        yield return null;
    }

    private IEnumerator UpdatePlayerCardCountText(int playerNumber, int cardCount)
    {
        // �����������, ��� � ��� ���� ������ ��������� ����� ��� ���������� ���� �������
        TMP_Text cardCountText = playerCardCountTexts[playerNumber - 1]; // �������� �� ���� �������� ��������� ����

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