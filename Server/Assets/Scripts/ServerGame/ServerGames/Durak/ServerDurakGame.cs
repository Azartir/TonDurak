using Mirror;
using System;
using System.Collections.Generic;

namespace TonDurakServer.Game
{
    public sealed class ServerDurakGame : ServerCardsGame
    {
        private int _maxCardCount = 6;
        private Suit _trumpSuit;

        private IDurakPlayer _currentAttacker;
        private IDurakPlayer _currentDefender;

        private List<Card> _tableCards = new List<Card>();
        private List<Card> _activeCards = new List<Card>();

        public override event Action OnGameStartedEvent;
        public override event Action OnGameEndedEvent;

        public override void AddPlayer(int id)
        {
            var player = new ServerDurakPlayer(id);
            player.OnPlayerTryAttackEvent += OnTryPlayerAttack;
            player.OnPlayerTryDefendEvent += OnTryPlayerDefend;
            player.OnPlayerTryTakeEvent += OnTryPlayerTake;
            player.OnPlayerTryTossEvent += OnTryPlayerToss;
            player.OnPlayerTryTransferEvent += OnTryPlayerTransfer;
            player.OnPlayerTryPassEvent += OnTryPlayerPass;
            serverPlayers.Add(player);
        }

        public override void RemovePlayer(int id)
        {
            foreach (var player in serverPlayers)
            {
                if (player.Id == id)
                {
                    var durakPlayer = (ServerDurakPlayer)player;
                    durakPlayer.OnPlayerTryAttackEvent -= OnTryPlayerAttack;
                    durakPlayer.OnPlayerTryDefendEvent -= OnTryPlayerDefend;
                    durakPlayer.OnPlayerTryTakeEvent -= OnTryPlayerTake;
                    durakPlayer.OnPlayerTryTossEvent -= OnTryPlayerToss;
                    durakPlayer.OnPlayerTryTransferEvent -= OnTryPlayerTransfer;
                    durakPlayer.OnPlayerTryPassEvent -= OnTryPlayerPass;
                    serverPlayers.Remove(player);
                    return;
                }
            }
        }

        public override void PreparePlayerPacket(int id, NetworkWriter writer) 
        {
            foreach (var player in serverPlayers)
                player.Serialize(id, writer);
        }

        public override void StartGame()
        {
            base.StartGame();

            cardDeck = InitializeCardDeck();
            cardDeck.Shuffle();
            
            _currentAttacker = (IDurakPlayer)serverPlayers[0];
            _currentDefender = (IDurakPlayer)serverPlayers[1];

            DealCards();
            ResetActivities();

            _trumpSuit = (Suit)UnityEngine.Random.Range(0, 3);

            Status = GameStatus.Running;
            OnGameStartedEvent?.Invoke();
        }

        public override void Tick()
        {
            base.Tick();
            bool checkRound = true;

            foreach (var player in serverPlayers)
            {
                if (player.Id == _currentDefender.Id)
                    checkRound &= _currentDefender.LastActivity == DurakActivity.Take || _currentDefender.LastActivity == DurakActivity.Defend;
                else
                {
                    var durakPlayer = (IDurakPlayer)player;
                    checkRound &= durakPlayer.LastActivity == DurakActivity.Passed;
                }
            }

            if (checkRound)
                OnRoundEnded();
        }

        private void OnRoundEnded()
        {
            _activeCards.Clear();

            int index = 0;

            if (_currentDefender.LastActivity == DurakActivity.Defend)
            {
                _currentAttacker = _currentDefender;

                index = serverPlayers.IndexOf((ServerPlayer)_currentAttacker);
                index = index == serverPlayers.Count - 1 ? 0 : index++;

                _currentDefender = (IDurakPlayer)serverPlayers[index];
            }
            else if (_currentDefender.LastActivity == DurakActivity.Take)
            {
                index = serverPlayers.IndexOf((ServerPlayer)_currentDefender);
                index = index == serverPlayers.Count - 1 ? 0 : index++;
                _currentAttacker = (IDurakPlayer)serverPlayers[index];
                index = index == serverPlayers.Count - 1 ? 0 : index++;
                _currentDefender = (IDurakPlayer)serverPlayers[index];
            }

            DealCards();

            ResetActivities();
        }

        private void ResetActivities()
        {
            foreach (var player in serverPlayers)
            {
                if (player.Id == _currentAttacker.Id)
                    _currentAttacker.LastActivity = DurakActivity.PrepareAttack;
                else if (player.Id == _currentDefender.Id)
                    _currentDefender.LastActivity = DurakActivity.PrepareDefence;
                else
                {
                    var durakPlayer = (IDurakPlayer)player;
                    durakPlayer.LastActivity = DurakActivity.Waiting;
                }
            }
        }

        protected override void DealCards()
        {
            if (_currentDefender == null)
            {
                foreach (var player in serverPlayers)
                {
                    IDurakPlayer durakPlayer = (IDurakPlayer)player;
                    DealToPlayer(durakPlayer);
                }
            }
            else
            {
                int index = serverPlayers.IndexOf((ServerPlayer)_currentDefender);
                var deffender = _currentDefender;

                if (deffender.LastActivity == DurakActivity.Toss)
                    index = index + 1 == serverPlayers.Count - 1 ? 0 : index++;
                else
                    index = index == serverPlayers.Count - 1 ? 0 : index;

                for (int i = index; i < serverPlayers.Count; i++)
                {
                    var player = (IDurakPlayer)serverPlayers[i];
                    DealToPlayer(player);
                }

                for (int j = 0; j < index; j++)
                {
                    var player = (IDurakPlayer)serverPlayers[j];
                    DealToPlayer(player);
                }
            }
        }

        private void DealToPlayer(IDurakPlayer player)
        {
            int cardsCount = player.CardsCount;

            if (cardsCount >= _maxCardCount)
                return;

            int neededCount = _maxCardCount - cardsCount;
            int deckCount = cardDeck.GetCardsCount();

            for (int i = 0; i < Math.Min(neededCount, deckCount); i++)
                player.AddCard(cardDeck.DrawCard());
        }

        private void CheckEndGame()
        {
            OnGameEndedEvent?.Invoke();
        }

        protected override ICardDeck InitializeCardDeck()
        {
            Stack<Card> cards = new Stack<Card>();

            cards.Push(new Card(Suit.Spades, Rank.Six));
            cards.Push(new Card(Suit.Spades, Rank.Seven));
            cards.Push(new Card(Suit.Spades, Rank.Eight));
            cards.Push(new Card(Suit.Spades, Rank.Nine));
            cards.Push(new Card(Suit.Spades, Rank.Ten));
            cards.Push(new Card(Suit.Spades, Rank.Jack));
            cards.Push(new Card(Suit.Spades, Rank.Queen));
            cards.Push(new Card(Suit.Spades, Rank.King));
            cards.Push(new Card(Suit.Spades, Rank.Ace));

            cards.Push(new Card(Suit.Diamonds, Rank.Six));
            cards.Push(new Card(Suit.Diamonds, Rank.Seven));
            cards.Push(new Card(Suit.Diamonds, Rank.Eight));
            cards.Push(new Card(Suit.Diamonds, Rank.Nine));
            cards.Push(new Card(Suit.Diamonds, Rank.Ten));
            cards.Push(new Card(Suit.Diamonds, Rank.Jack));
            cards.Push(new Card(Suit.Diamonds, Rank.Queen));
            cards.Push(new Card(Suit.Diamonds, Rank.King));
            cards.Push(new Card(Suit.Diamonds, Rank.Ace));

            cards.Push(new Card(Suit.Hearts, Rank.Six));
            cards.Push(new Card(Suit.Hearts, Rank.Seven));
            cards.Push(new Card(Suit.Hearts, Rank.Eight));
            cards.Push(new Card(Suit.Hearts, Rank.Nine));
            cards.Push(new Card(Suit.Hearts, Rank.Ten));
            cards.Push(new Card(Suit.Hearts, Rank.Jack));
            cards.Push(new Card(Suit.Hearts, Rank.Queen));
            cards.Push(new Card(Suit.Hearts, Rank.King));
            cards.Push(new Card(Suit.Hearts, Rank.Ace));

            cards.Push(new Card(Suit.Clumbs, Rank.Six));
            cards.Push(new Card(Suit.Clumbs, Rank.Seven));
            cards.Push(new Card(Suit.Clumbs, Rank.Eight));
            cards.Push(new Card(Suit.Clumbs, Rank.Nine));
            cards.Push(new Card(Suit.Clumbs, Rank.Ten));
            cards.Push(new Card(Suit.Clumbs, Rank.Jack));
            cards.Push(new Card(Suit.Clumbs, Rank.Queen));
            cards.Push(new Card(Suit.Clumbs, Rank.King));
            cards.Push(new Card(Suit.Clumbs, Rank.Ace));

            IShuffleStrategy strategy = new FisherYatesShuffle();
            
            return new CardDeck(cards, strategy);
        }

        private bool ValidateDefend(Card defenderCard, Suit activeSuit, Rank activeRank, out Card activeCard)
        {
            foreach (var card in _activeCards)
                if (card.Rank == activeRank && card.Suit == activeSuit)
                {
                    if (card.Rank > defenderCard.Rank && card.Suit == defenderCard.Suit)
                    {
                        activeCard = card;
                        return true;
                    }
                    else if (defenderCard.Suit == _trumpSuit && card.Suit != _trumpSuit)
                    {
                        activeCard = card;
                        return true;
                    }
                    else if (defenderCard.Suit == _trumpSuit && card.Suit == _trumpSuit && defenderCard.Rank > card.Rank)
                    {
                        activeCard = card;
                        return true;
                    }
                }

            activeCard = null;
            return false;
        }

        private bool ValidateToss(Card tossCard)
        {
            foreach (var activeCard in _tableCards)
                if (tossCard.Rank == activeCard.Rank)
                    return true;

            return false;
        }

        private void OnTryPlayerAttack(IDurakPlayer player, AttackPacket packet)
        {
            if (player.ContainsCard(packet.Suit, packet.Rank, out Card card))
            {
                player.RemoveCard(card);
                _tableCards.Add(card);
                _activeCards.Remove(card);
                player.LastActivity = DurakActivity.Attack;
            }    
        }

        private void OnTryPlayerDefend(IDurakPlayer player, DefendPacket packet)
        {
            if (player.ContainsCard(packet.MySuit, packet.MyRank, out Card card))
            {
                if (ValidateDefend(card, packet.OtherSuit, packet.OtherRank, out Card activeCard))
                {
                    player.RemoveCard(card);
                    _tableCards.Add(card);
                    _tableCards.Add(activeCard);
                    _activeCards.Remove(activeCard);
                    player.LastActivity = DurakActivity.Defend;
                }
            }
        }

        private void OnTryPlayerTake(IDurakPlayer player, TakePacket packet)
        {
            foreach (var card in _tableCards)
                player.AddCard(card);

            player.LastActivity = DurakActivity.Take;
        }

        private void OnTryPlayerToss(IDurakPlayer player, TossPacket packet)
        {
            if (player.ContainsCard(packet.Suit, packet.Rank, out Card card))
            {
                if (ValidateToss(card))
                {
                    player.RemoveCard(card);
                    _tableCards.Add(card);
                    player.LastActivity = DurakActivity.Defend;
                }
            }
        }

        private void OnTryPlayerPass(IDurakPlayer player, PassPacket packet)
        {
            if (player == _currentAttacker)
                player.LastActivity = DurakActivity.Passed;
            else if (player != _currentDefender)
                player.LastActivity = DurakActivity.Passed;
        }

        private void OnTryPlayerTransfer(IDurakPlayer player, TransferPacket packet)
        {

        }
    }
}
