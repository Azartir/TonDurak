//using CardsInventorySystem;
//using CardsInventorySystem.CardsSystem;
//using CardsInventorySystem.View;
//using MultiplayerSystem.CustomData;
//using PlayersSystem;
//using SaveSystem;
//using TableSystem;
//using UnityEngine;
//using UnityEngine.EventSystems;

//namespace GameSystem
//{
//    public class ClickableCardHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
//    {
//        private CardsInventoryHandler _cardsHandler;

//        private Transform _cachedTransform;

//        private CardView _cardView;
//        public CardData CardData => _cardView.CardData;
//        public PlayerHandler Player => _cardsHandler.Player;

//        private void Start()
//            => _cardView = GetComponent<CardView>();

//        public void Init(CardsInventoryHandler cardsHandler)
//            => _cardsHandler = cardsHandler;

//        private bool CanPlaceCard()
//        {
//            if (_cardsHandler.Player.PassPressed) return false;
//            if (GameTypeMarker.Singleton.GameType == GameType.Classic)
//            {
//                if (!_cardsHandler.Player.IsMoving) return false;
//            }
//            else
//            {
//                if (_cardsHandler.Player.IsMoving) return true;
//                if (_cardsHandler.Player.IsThrows && TableCardsHandler.Singleton.Cards.Length > 0) return true;
//                return false;
//            }

//            return true;
//        }

//        public void TryPlaceOnTable()
//        {
//            if (!CanPlaceCard()) return;
//            var cardView = GetComponent<CardView>();
//            var cardData = new NetworkCardData(cardView.CardData.Value, cardView.CardData.Type);
//            if (TableCardsHandler.Singleton.TryAddCard(cardData))
//            {
//                _cardsHandler.RemoveCard(cardData);
//                Destroy(gameObject);
//            }
//        }

//        public void Destroy()
//            => Destroy(gameObject);

//        public void OnBeginDrag(PointerEventData eventData)
//        {
//            _cachedTransform = transform.parent;
//            transform.SetParent(transform.root);
//            _cardView.HandleRayTarget(false);
//        }

//        public void OnDrag(PointerEventData eventData)
//        {
//            transform.position = eventData.position;
//        }

//        public void OnEndDrag(PointerEventData eventData)
//        {
//            transform.SetParent(_cachedTransform);
//            _cardView.HandleRayTarget(true);
//        }
//    }
//}