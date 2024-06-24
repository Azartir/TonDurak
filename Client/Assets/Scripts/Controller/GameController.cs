using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private LobbyManager lobbyInfoProvider;
    [SerializeField] private CardPool cardPool;
    [Header("gameObjects")]
    [SerializeField] private GameObject[] playerObjectsMove;
    [SerializeField] private GameObject[] playerObjects;
    [SerializeField] private TMP_Text[] playerNameTexts;
    [SerializeField] private Image[] playerIcons;
    [SerializeField] private TMP_Text[] playerCardCountTexts;
    [SerializeField] private Image trumCard;
    [SerializeField] private TMP_Text deckCardCountText;
    [SerializeField] private GameObject deck;

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
        int playerCount = lobbyInfoProvider.GetPlayerCount();
        Dictionary<int, string> playerNames = lobbyInfoProvider.GetPlayerName();
        Dictionary<int, byte[]> iconData = lobbyInfoProvider.GetPlayersIcon();
        Dictionary<int, Sprite> playerIcons = new Dictionary<int, Sprite>();

        for (int i = 0; i < playerObjects.Length; i++)
        {
            if (i < playerCount)
            {
                playerObjects[i].SetActive(true);
                playerNameTexts[i].text = playerNames.ContainsKey(i + 1) ? playerNames[i + 1] : "Unknown Player";

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
        int playerCount = lobbyInfoProvider.GetPlayerCount();
        SimpleCard trumpCard = lobbyInfoProvider.GetTrumbCard();
        int currentTurnPlayer = lobbyInfoProvider.GetPlayerMove();
        SimpleCard[] availableTurnCards = lobbyInfoProvider.GetCurrentAvailableTurnCard();
        Dictionary<int, string> playerNames = lobbyInfoProvider.GetPlayerName();
        Dictionary<int, int> playerCardCounts = lobbyInfoProvider.GetPlayerCardCounts();
        int deckCardsCount = lobbyInfoProvider.GetDeckCardsCount();

        yield return StartCoroutine(UpdateTrumpCardImage(trumpCard));
        //yield return StartCoroutine(ActivateObjectsByCurrentPlayer(currentTurnPlayer));
        //yield return StartCoroutine(AddCardDragAndDropToAvailableCards(availableTurnCards));

        for (int i = 0; i < playerCount; i++)
        {
            yield return StartCoroutine(UpdatePlayerCardCountText(i + 1, playerCardCounts.ContainsKey(i + 1) ? playerCardCounts[i + 1] : 0));
        }

        yield return StartCoroutine(UpdateDeckCardCountText(deckCardsCount));
    }

    private IEnumerator UpdateTrumpCardImage(SimpleCard trumpCard)
    {
        GameObject card = cardPool.GetCard(trumpCard.ToCardCode());
        if (card != null)
        {
            trumCard.sprite = card.GetComponent<Image>().sprite;
        }
        else
        {
            Debug.LogError("Trump card prefab is missing.");
        }
        yield return null;
    }

    private IEnumerator UpdatePlayerCardCountText(int playerIndex, int cardCount)
    {
        if (playerCardCountTexts.Length > playerIndex - 1)
        {
            playerCardCountTexts[playerIndex - 1].text = cardCount.ToString();
        }
        yield return null;
    }

    private IEnumerator UpdateDeckCardCountText(int deckCardsCount)
    {
        if (deckCardCountText != null)
        {
            deckCardCountText.text = deckCardsCount.ToString();
        }
        if (deckCardsCount == 0)
        {
            deck.SetActive(false);
        }
        yield return null;
    }

    private IEnumerator ActivateObjectsByCurrentPlayer(int currentPlayerIndex)
    {
        if (playerObjectsMove.Length > currentPlayerIndex - 1)
        {
            playerObjectsMove[currentPlayerIndex - 1].SetActive(true);
        }
        else
        {
            Debug.LogError("Invalid player index: " + currentPlayerIndex);
        }
        yield return null;
    }
}
