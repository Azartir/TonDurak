using System;
using System.Collections.Generic;
using CardsInventorySystem.CardsSystem;
using MultiplayerSystem.CustomData;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PlayersSystem
{
    [RequireComponent(typeof(Avatar))]
    public class PlayerHandler : MonoBehaviour
    {
        [Header("Attached Components")] 

        //[SerializeField] private CardsInventoryHandler _cardsInventoryHandler;

        [Header("Main Value")] 

        [SerializeField] private int _id;
        [SerializeField] private bool _isMoving;
        [SerializeField] private bool _isDefending;
        [SerializeField] private bool _isThrows;
        [SerializeField] private bool _passPressed;

        public Action OnPressedTake { get; set; }
        public Action OnPressPass { get; set; }

        public bool IsDefending
        {
            get => _isDefending;
            set => _isDefending = value;
        }

        public bool IsMoving
        {
            get => _isMoving;
            set
            {
                _isMoving = value;
                //DisplayActiveSync();
                //InvokeRemoteMethod("DisplayActiveSync");
            }
        }

        public bool PassPressed
        {
            get => _passPressed;
            set => _passPressed = value;
        }
        
        public bool IsThrows { get => _isThrows; set => _isThrows = value; }
        
        //public bool IsWin => _cardsInventoryHandler.Cards.Length == 0;
        public int Id => _id;
        //public bool IsMine => _avatar.IsMe;

        //private Avatar _avatar;
        //private BasePlayerView _playerView;

        //public CardsInventoryHandler CardsInventoryHandler => _cardsInventoryHandler;

        private void Awake()
        {
        //    _avatar = GetComponent<Avatar>();
        }

        private void Start()
        {
            //if (Multiplayer.GetUser().IsHost)
            //    _id = Random.Range(1, 65535);
            //GlobalEventsContainer.OnPlayerSpawned?.Invoke(this);
            //if (GetComponent<Avatar>().IsMe)
            //    GlobalEventsContainer.OnMyPlayerSpawned?.Invoke(this);
        }

        //private void DisplayActiveSync()
        //    => _playerView.HandleActiveMovePanel(_isMoving);

        //public void AssignView(BasePlayerView playerView)
        //{
        //    _playerView = playerView;
        //    _cardsInventoryHandler.Init(playerView, this);
        //    _playerView.Init(this);
        //}

        //public void AddCard(CardData cardData)
        //    => _cardsInventoryHandler.AddCard(cardData);

        //public void RemoveCard(NetworkCardData cardData)
        //    => _cardsInventoryHandler.RemoveCard(cardData);

        //public NetworkCardData GetMinimalMajorCard()
        //    => _cardsInventoryHandler.GetMinimalMajorCard();

        public void TakeCardsFromTable()
        {
            if(IsMoving) return;
            //var tableCardsHandler = TableCardsHandler.Singleton;
            //var tableCards = tableCardsHandler.Cards;
            var fixedList = new List<NetworkCardData>();
            //foreach (var card in tableCards)
            //{
            //    fixedList.Add(card.CurrentCardData);
            //    if (card.BitCardData.Id != -1)
            //        fixedList.Add(card.BitCardData);
            //}

            //_cardsInventoryHandler.AddCards(fixedList.ToArray());
            //TableCardsHandler.Singleton.ClearCards();
        }

        private void HandlePassButtonSync()
            => OnPressPass?.Invoke();


        private void HandleTakeButtonSync()
            => OnPressedTake?.Invoke();

        //public void HandlePassButton()
        //    => InvokeRemoteMethod("HandlePassButtonSync");

        //public void HandleTakeButton()
        //    => InvokeRemoteMethod("HandleTakeButtonSync");

        public void HandleGameEnd()
        {
            //HandleGameEndSync();
            //InvokeRemoteMethod("HandleGameEndSync");
        }

        //private void HandleGameEndSync()
        //    => GlobalEventsContainer.OnPlayerWin?.Invoke(this);
    }
}