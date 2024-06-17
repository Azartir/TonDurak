//using System;
//using System.Collections.Generic;
//using System.Linq;
//using CardsInventorySystem.CardsSystem;
//using Events;
//using MultiplayerSystem.CustomData;
//using UnityEngine;

//namespace CardsInventorySystem
//{
//    public class GlobalCardsContainer : MonoBehaviour
//    {
//        public static GlobalCardsContainer Singleton { get; set; }
        
//        private readonly string _cardsResourceFolder = "Cards/";

//        [SerializeField] private List<CardData> _cards = new List<CardData>();

//        private void Awake()
//            => Singleton = this;
        
//        private void Start()
//            => LoadCards();

//        private void LoadCards()
//        {
//            _cards = Resources.LoadAll<CardData>(_cardsResourceFolder).ToList();
//            GlobalEventsContainer.OnCardsLoaded?.Invoke(_cards);
//        }

//        public CardData GetCardData(NetworkCardData cardData)
//        {
//            foreach (var card in _cards)
//            {
//                if(card.Value != cardData.Id || card.Type != cardData.CardType) continue;
//                return card;
//            }

//            throw new Exception("Can't find card: " + cardData.Id + " " + cardData.CardType.ToString());
//        }
//    }
//}