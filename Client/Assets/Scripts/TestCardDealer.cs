using UnityEngine;

public class TestCardDealer : MonoBehaviour
{
    [SerializeField] private CardDealer cardDealer;
    [SerializeField] private string[] cardNames = new string[]
    {
        "6_H", "7_H", "8_H", "9_H", "10_H", "J_H"
    };

    private void Start()
    {
        if (cardDealer == null)
        {
            Debug.LogError("CardDealer is not assigned.");
            return;
        }

        cardDealer.DealCards(cardNames);
    }
}
