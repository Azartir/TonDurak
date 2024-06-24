using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class GameController : MonoBehaviour
{
    [SerializeField] private LobbyManager lobbyInfoProvider; // ������ �� ��������� ���������� �����
    [SerializeField] private CardPool cardPool;
    [Header("gameObjects")]
    [SerializeField] private GameObject[] playerObjectsMove;
    [SerializeField] private GameObject[] playerObjects; // ������ �� ������� �������
    [SerializeField] private TMP_Text[] playerNameTexts; // ������ �� ��������� ���� ��� ���� �������
    [SerializeField] private Image[] playerIcons; // ������ �� ������ �������
    [SerializeField] private TMP_Text[] playerCardCountTexts; // ������ �� ��������� ���� ��� ���������� ���� � �������
    [SerializeField] private Image trumCard;
    [SerializeField] private TMP_Text deckCardCountText; // ������ �� ��������� ���� ��� ���������� ���� � ������
    [SerializeField] private Transform deck;

    private void Start()
    {
        if (lobbyInfoProvider != null)
        {
            StartCoroutine(SetupPlayers());
        }
        else
        {
            Debug.LogError("LobbyInfoManager component is missing.");
        }
    }

    private IEnumerator SetupPlayers()
    {
        // �������� ���������� �������
        int playerCount = lobbyInfoProvider.GetPlayerCount();
        Debug.Log("Player Count: " + playerCount);

        // �������� ����� �������
        Dictionary<int, string> playerNames = lobbyInfoProvider.GetPlayerName();

        // �������� ������ �������
        Dictionary<int, byte[]> iconData = lobbyInfoProvider.GetPlayersIcon();
        Dictionary<int, Sprite> playerIcons = new Dictionary<int, Sprite>();
        foreach (var item in iconData)
        {
            //playerIcons[item.Key] = ConvertToSprite(item.Value);
        }

        // ����������� ������� �������
        for (int i = 0; i < playerObjects.Length; i++)
        {
            if (i < playerCount)
            {
                playerObjects[i].SetActive(true);
                playerNameTexts[i].text = playerNames.ContainsKey(i + 1) ? playerNames[i + 1] : "Unknown Player";

                // ������������� ������ ������
                if (playerIcons.ContainsKey(i + 1))
                {
                    this.playerIcons[i].sprite = playerIcons[i + 1];
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

        yield return StartCoroutine(DisplayLobbyInfo());
    }

    private IEnumerator DisplayLobbyInfo()
    {
        // �������� � ���������� ������
        int playerCount = lobbyInfoProvider.GetPlayerCount();
        SimpleCard trumpCard = lobbyInfoProvider.GetTrumbCard();
        int currentTurnPlayer = lobbyInfoProvider.GetPlayerMove();
        SimpleCard[] availableTurnCards = lobbyInfoProvider.GetCurrentAvailableTurnCard();
        Dictionary<int, string> playerNames = lobbyInfoProvider.GetPlayerName();
        Dictionary<int, int> playerCardCounts = lobbyInfoProvider.GetPlayerCardCounts();
        int deckCardsCount = lobbyInfoProvider.GetDeckCardsCount();

        // ����� � ���
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

        // ��������� ���������
        yield return StartCoroutine(UpdateTrumpCardImage(trumpCard));
        yield return StartCoroutine(ActivateObjectsByCurrentPlayer(currentTurnPlayer));
        yield return StartCoroutine(AddCardDragAndDropToAvailableCards(availableTurnCards));

        // ��������� ���������� ���� � ������� ������
        for (int i = 0; i < playerCount; i++)
        {
            yield return StartCoroutine(UpdatePlayerCardCountText(i + 1, playerCardCounts.ContainsKey(i + 1) ? playerCardCounts[i + 1] : 0));
        }

        // ��������� ���������� ���� � ������
        yield return StartCoroutine(UpdateDeckCardCountText(deckCardsCount));
    }

    private IEnumerator UpdateTrumpCardImage(SimpleCard trumpCard)
    {
        // �������� ����� �� ���� �� �����
        GameObject card = cardPool.GetCard(trumpCard.ToString());

        if (card == null)
        {
            Debug.LogWarning("Card not found in pool: " + trumpCard);
            yield break;
        }

        // ������������� ����� �������� ���������
        card.transform.SetParent(deck, false);
        card.transform.localPosition = Vector3.zero;
        card.transform.localRotation = Quaternion.Euler(0, 0, 0);

        // �������� �������� �����
        card.transform.DORotate(new Vector3(0, 60, 0), 1, RotateMode.FastBeyond360)
            .SetEase(Ease.OutQuad);

        yield return new WaitForSeconds(1);
    }

    private IEnumerator ActivateObjectsByCurrentPlayer(int currentPlayerNumber)
    {
        for (int i = 0; i < playerObjectsMove.Length; i++)
        {
            playerObjectsMove[i].SetActive(i + 1 == currentPlayerNumber);
        }

        yield return null;
    }

    private IEnumerator AddCardDragAndDropToAvailableCards(SimpleCard[] availableCards)
    {
        foreach (SimpleCard card in availableCards)
        {
            GameObject cardObject = cardPool.GetCard(card.ToString());

            if (cardObject != null)
            {
                cardObject.SetActive(true);

                CardDragAndDrop dragAndDropComponent = cardObject.GetComponent<CardDragAndDrop>();
                if (dragAndDropComponent == null)
                {
                    dragAndDropComponent = cardObject.AddComponent<CardDragAndDrop>();
                }
                else
                {
                    Debug.Log("Card object already has CardDragAndDrop component: " + card);
                }
            }
            else
            {
                Debug.LogWarning("Card object not found in pool: " + card);
            }
        }

        yield return null;
    }

    private IEnumerator UpdatePlayerCardCountText(int playerNumber, int cardCount)
    {
        // ��������� ������������ ������� playerNumber
        if (playerNumber < 1 || playerNumber > playerCardCountTexts.Length)
        {
            Debug.LogWarning("Player number " + playerNumber + " is out of range for playerCardCountTexts array.");
            yield break;
        }

        TMP_Text cardCountText = playerCardCountTexts[playerNumber - 1];
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

    // ��������������� ����� ��� �������������� byte[] � Sprite
    //private Sprite ConvertToSprite(byte[] imageData)
    //{
    //    if (imageData.Length == 0)
    //    {
    //        Debug.LogWarning("Image data is empty.");
    //        return null;
    //    }

    //    Texture2D texture = new Texture2D(2, 2);
    //    if (texture.LoadImage(imageData))
    //    {
    //        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    //    }

    //    Debug.LogWarning("Failed to create texture from image data.");
    //    return null;
    //}
}
