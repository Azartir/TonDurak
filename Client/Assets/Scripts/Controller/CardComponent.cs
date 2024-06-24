using UnityEngine;

public class CardComponent : MonoBehaviour
{
    public SimpleCard cardData; // This should be a field or property within CardComponent

    public void SetCardData(SimpleCard data)
    {
        cardData = data;
    }

    public SimpleCard GetCardData()
    {
        return cardData;
    }
}
