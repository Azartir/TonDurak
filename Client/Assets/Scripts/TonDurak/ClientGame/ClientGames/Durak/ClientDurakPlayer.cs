using Mirror;
using System;

namespace TonDurakClient.Game
{
    public class ClientDurakPlayer : ClientCardPlayer, IDurakPlayer
    {
        private AttackPacket _attackPacket;
        private DefendPacket _defendPacket;
        private TakePacket _takePacket;
        private TossPacket _tossPacket;
        private TransferPacket _transferPacket;
        private PassPacket _passPacket;

        public DurakActivity LastActivity { get; set; }

        public event Action<IDurakPlayer> OnObserverAttackEvent;
        public event Action<IDurakPlayer> OnObserverDefendEvent;
        public event Action<IDurakPlayer> OnObserverTakeEvent;
        public event Action<IDurakPlayer> OnObserverTossEvent;
        public event Action<IDurakPlayer> OnObserverTransferEvent;
        public event Action<IDurakPlayer> OnObserverPassEvent;
        public event Action<IDurakPlayer> OnObserverPrepareAttackEvent;
        public event Action<IDurakPlayer> OnObserverPrepareDefendEvent;
        public event Action<IDurakPlayer> OnObserverPrepareTossEvent;
        public event Action<IDurakPlayer> OnObserverWaitingEvent;


        public void OnAttack(SimpleCard card)
        {
            _attackPacket.HasCriticalData = true;
            _attackPacket.Suit = card.Suit;
            _attackPacket.Rank = card.Rank;
        }

        public void OnDefend(SimpleCard myCard, SimpleCard otherCard)
        {
            _defendPacket.HasCriticalData = true;
            _defendPacket.MyRank = myCard.Rank;
            _defendPacket.MySuit = myCard.Suit;
            _defendPacket.OtherRank = otherCard.Rank;
            _defendPacket.OtherSuit = otherCard.Suit;
        }

        public void OnTake()
        {
            _takePacket.HasCriticalData = true;
        }

        public void OnToss(SimpleCard card)
        {
            _tossPacket.HasCriticalData = true;
            _tossPacket.Suit = card.Suit;
            _tossPacket.Rank = card.Rank;
        }

        public void OnTransfer(SimpleCard card)
        {
            _transferPacket.HasCriticalData = true;
            _transferPacket.Rank = card.Rank;
            _transferPacket.Suit = card.Suit;
        }

        public void OnPass()
        {
            _passPacket.HasCriticalData = true;
        }

        public override void Serialize(NetworkWriter writer)
        {
            writer.WriteBool(_attackPacket.HasCriticalData);
            if (_attackPacket.HasCriticalData)
            {
                writer.WriteByte((byte)_attackPacket.Suit);
                writer.WriteByte((byte)_attackPacket.Rank);
            }

            writer.WriteBool(_defendPacket.HasCriticalData);
            if (_defendPacket.HasCriticalData)
            {
                writer.WriteByte((byte)_defendPacket.MySuit);
                writer.WriteByte((byte)_defendPacket.MyRank);
                writer.WriteByte((byte)_defendPacket.OtherSuit);
                writer.WriteByte((byte)_defendPacket.OtherRank);
            }

            writer.WriteBool(_takePacket.HasCriticalData);

            writer.WriteBool(_tossPacket.HasCriticalData);
            if (_tossPacket.HasCriticalData)
            {
                writer.WriteByte((byte)_tossPacket.Suit);
                writer.WriteByte((byte)_tossPacket.Rank);
            }

            writer.WriteBool(_transferPacket.HasCriticalData);
            if (_transferPacket.HasCriticalData)
            {
                writer.WriteByte((byte)_transferPacket.Suit);
                writer.WriteByte((byte)_transferPacket.Rank);
            }

            writer.WriteBool(_passPacket.HasCriticalData);

            _attackPacket.HasCriticalData = false;
            _defendPacket.HasCriticalData = false;
            _takePacket.HasCriticalData = false;
            _transferPacket.HasCriticalData = false;
            _passPacket.HasCriticalData = false;
        }

        public override void Deserialize(NetworkReader reader)
        {
            bool isMe = reader.ReadBool();

            if (isMe)
            {
                this.isMe = true;
                int cardsCount = reader.ReadInt();
                hand.Clear();
                for (int i = 0; i < cardsCount; i++)
                {
                    var card = new SimpleCard()
                    {
                        Rank = (Rank)reader.ReadByte(),
                        Suit = (Suit)reader.ReadByte(),
                    };

                    hand.Add(card);
                }
                LastActivity = (DurakActivity)reader.ReadByte();
            }
            else
            {
                this.isMe = false;
                int cardsCount = reader.ReadInt();
                hand.Clear();
                for (int i = 0; i < cardsCount; i++)
                    hand.Add(new SimpleCard());
                DurakActivity newActivity = (DurakActivity)reader.ReadByte();
                if (LastActivity != newActivity)
                    OnObserverNewActivity(newActivity);
            } 
        }

        private void OnObserverNewActivity(DurakActivity activity)
        {
            LastActivity = activity;

            switch (activity)
            {
                case DurakActivity.Attack:
                    OnObserverAttackEvent?.Invoke(this);
                    break;
                case DurakActivity.Defend:
                    OnObserverDefendEvent?.Invoke(this);
                    break;
                case DurakActivity.Take:
                    OnObserverTakeEvent?.Invoke(this);
                    break;
                case DurakActivity.Toss:
                    OnObserverTossEvent?.Invoke(this);
                    break;
                case DurakActivity.Transfer:
                    OnObserverTransferEvent?.Invoke(this);
                    break;
                case DurakActivity.Passed:
                    OnObserverPassEvent?.Invoke(this);
                    break;
                case DurakActivity.PrepareAttack:
                    OnObserverPrepareAttackEvent?.Invoke(this);
                    break;
                case DurakActivity.PrepareDefence:
                    OnObserverPrepareDefendEvent?.Invoke(this);
                    break;
                case DurakActivity.PrepareToss:
                    OnObserverPrepareTossEvent?.Invoke(this);
                    break;
                case DurakActivity.Waiting:
                    OnObserverWaitingEvent?.Invoke(this);
                    break;
            }
        }
    }

    public interface IDurakPlayer
    {
        event Action<IDurakPlayer> OnObserverAttackEvent;
        event Action<IDurakPlayer> OnObserverDefendEvent;
        event Action<IDurakPlayer> OnObserverTakeEvent;
        event Action<IDurakPlayer> OnObserverTossEvent;
        event Action<IDurakPlayer> OnObserverTransferEvent;
        event Action<IDurakPlayer> OnObserverPassEvent;
        event Action<IDurakPlayer> OnObserverPrepareAttackEvent;
        event Action<IDurakPlayer> OnObserverPrepareDefendEvent;
        event Action<IDurakPlayer> OnObserverPrepareTossEvent;
        event Action<IDurakPlayer> OnObserverWaitingEvent;
        void OnAttack(SimpleCard card);
        void OnDefend(SimpleCard myCard, SimpleCard otherCard);
        void OnTake();
        void OnToss(SimpleCard card);
        void OnTransfer(SimpleCard card);
        void OnPass();
    }

    public enum DurakActivity
    {
        None,
        Attack,
        Defend,
        Take,
        Toss,
        Transfer,
        Passed,
        Waiting,
        PrepareAttack,
        PrepareDefence,
        PrepareToss,
    }
}
