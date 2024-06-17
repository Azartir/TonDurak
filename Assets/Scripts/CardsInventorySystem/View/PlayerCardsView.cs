//using System.Collections.Generic;
//using GameSystem;
//using MultiplayerSystem.CustomData;
//using PlayersSystem;
//using Unity.VisualScripting;
//using UnityEngine;

//namespace CardsInventorySystem.View
//{
//    public class PlayerCardsView : MonoBehaviour
//    {
//        [Header("Attached Components")]
//        [SerializeField] private CardsLayout _cardsLayout;
        
//        [Header("Cards Spawn")]
//        [SerializeField] private Transform _placeForCards;
//        [SerializeField] private CardView _cardPref;

//        private List<CardView> _cards = new List<CardView>();

//        private PlayerHandler _playerHandler;

//        public void Init(PlayerHandler playerHandler)
//            => _playerHandler = playerHandler;

//        private void ClearPlace(Transform targetPlace)
//        {
//            _cards.Clear();
//            foreach (Transform child in targetPlace)
//                Destroy(child.gameObject);
//        }

//        private CardView GetSpawnedCard(NetworkCardData cardData)
//        {
//            var instance = Instantiate(_cardPref, _placeForCards);
//            instance.Init(cardData);
//            return instance;
//        }

//        public void DisplayCards(NetworkCardData[] data)
//        {
//            ClearPlace(_placeForCards);
//            foreach (var cardData in data)
//            {
//                var card = GetSpawnedCard(cardData);
//                var clickable = card.AddComponent<ClickableCardHandler>();
//                clickable.Init(_playerHandler.CardsInventoryHandler);
//                _cards.Add(card);
//            }
            
//            //_cardsLayout.LayoutCards(_cards);
//        }
//    }
//}