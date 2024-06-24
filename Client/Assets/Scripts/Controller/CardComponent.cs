using UnityEngine;

public class CardComponent : MonoBehaviour
{
    public SimpleCard cardData;

    public void SetCardData(SimpleCard data)
    {
        cardData = data;
    }

    public SimpleCard GetCardData()
    {
        return cardData;
    }
    private void Update()
    {
        transform.localScale = new Vector3(2, 3, 1);
    }
}
