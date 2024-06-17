using UnityEngine;

namespace CardsInventorySystem.CardsSystem
{
    [System.Serializable]
    [CreateAssetMenu(menuName = "Custom/Card")]
    public class CardData : ScriptableObject
    {
        [SerializeField] private int _value = 0;
        [SerializeField] private CardType _cardType = CardType.Heart;
        [SerializeField] private Sprite _cardImage;

        public int Value => _value;
        public CardType Type => _cardType;
        public Sprite CardImage => _cardImage;
    }
}