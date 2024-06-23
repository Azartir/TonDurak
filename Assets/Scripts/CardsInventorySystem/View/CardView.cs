//using CardsInventorySystem.CardsSystem;
//using MultiplayerSystem.CustomData;
//using UnityEngine;
//using UnityEngine.UI;

//namespace CardsInventorySystem.View
//{
//    public class CardView : MonoBehaviour
//    {
//        [SerializeField] private Image _cardImage;
        
//        private CardData _cardData;

//        public CardData CardData => _cardData;

//        public void Init(NetworkCardData networkCardData)
//        {
//            _cardData = GlobalCardsContainer.Singleton.GetCardData(networkCardData);
//            _cardImage.sprite = _cardData.CardImage;
//        }

//        public void HandleRayTarget(bool value)
//            => _cardImage.raycastTarget = value;
//    }
//}