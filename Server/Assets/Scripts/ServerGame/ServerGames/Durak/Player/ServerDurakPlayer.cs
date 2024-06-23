using Mirror;
using System;

namespace TonDurakServer.Game
{
    public sealed class ServerDurakPlayer : ServerCardPlayer, IDurakPlayer
    {
        public ServerDurakPlayer(int id) : base(id)
        {
        }

        public DurakActivity LastActivity { get; set; }

        public event Action<IDurakPlayer, AttackPacket> OnPlayerTryAttackEvent;
        public event Action<IDurakPlayer, DefendPacket> OnPlayerTryDefendEvent;
        public event Action<IDurakPlayer, TakePacket> OnPlayerTryTakeEvent;
        public event Action<IDurakPlayer, TossPacket> OnPlayerTryTossEvent;
        public event Action<IDurakPlayer, TransferPacket> OnPlayerTryTransferEvent;
        public event Action<IDurakPlayer, PassPacket> OnPlayerTryPassEvent;


        public void AddCard(Card card)
        {
            hand.Add(card);
        }

        public void RemoveCard(Card card)
        {
            hand.Remove(card);
        }

        public void RemoveCardFromHand(Card card)
        {
            hand.Remove(card);
        }

        public override void Deserialize(NetworkReader reader)
        {
            base.Deserialize(reader);
            ApplyPlayerPacket(reader);
        }

        public override void Serialize(int id, NetworkWriter networkWriter)
        {
            base.Serialize(id, networkWriter);

            if (id == Id)
            {
                networkWriter.WriteInt(id);
                networkWriter.WriteInt(CardsCount);
                foreach (var card in hand)
                {
                    networkWriter.Write(card.Rank);
                    networkWriter.Write(card.Suit);
                }
                networkWriter.Write(LastActivity);
            }
            else
            {
                networkWriter.WriteInt(id);
                networkWriter.WriteInt(CardsCount);
                networkWriter.Write(LastActivity);
            }
        }

        private void ApplyPlayerPacket(NetworkReader reader)
        {
            bool attackCriticalData = reader.ReadBool();
            if (attackCriticalData)
            {
                var attackPacket = ApplyAttackPacket(reader);
                OnPlayerTryAttackEvent?.Invoke(this, attackPacket);
            }

            bool defendCriticalData = reader.ReadBool();
            if (defendCriticalData)
            {
                var defendPacket = ApplyDeffendPacket(reader);
                OnPlayerTryDefendEvent?.Invoke(this, defendPacket);
            }

            bool takeCriticalData = reader.ReadBool();
            if (takeCriticalData)
            {
                var takePacket = ApplyTakePacket(reader);
                OnPlayerTryTakeEvent?.Invoke(this, takePacket);
            }
            bool tossCriticalData = reader.ReadBool();
            if (tossCriticalData)
            {
                var tossPacket = ApplyTossPacket(reader);
                OnPlayerTryTossEvent?.Invoke(this, tossPacket);
            }
            bool transferCriticalData = reader.ReadBool();
            if (transferCriticalData)
            {
                var transferPacket = ApplyTransferPacket(reader);
                OnPlayerTryTransferEvent?.Invoke(this, transferPacket);
            }
            bool passCriticalData = reader.ReadBool();
            if (passCriticalData)
            {
                var passPacket = ApplyPassPacket(reader);
                OnPlayerTryPassEvent?.Invoke(this, passPacket);
            }
        }

        private AttackPacket ApplyAttackPacket(NetworkReader reader)
        {
            return new AttackPacket()
            {
                Suit = reader.Read<Suit>(),
                Rank = reader.Read<Rank>(),
            };
        }

        private DefendPacket ApplyDeffendPacket(NetworkReader reader)
        {
            return new DefendPacket()
            {
                MySuit = reader.Read<Suit>(),
                MyRank = reader.Read<Rank>(),
            };
        }

        private TakePacket ApplyTakePacket(NetworkReader reader)
        {
            return new TakePacket()
            {
                HasCriticalData = true
            };
        }

        private TossPacket ApplyTossPacket(NetworkReader reader)
        {
            return new TossPacket()
            {
                Suit = reader.Read<Suit>(),
                Rank = reader.Read<Rank>(),
            };
        }

        private TransferPacket ApplyTransferPacket(NetworkReader reader)
        {
            return new TransferPacket()
            {
                Suit = reader.Read<Suit>(),
                Rank = reader.Read<Rank>(),
            };
        }

        private PassPacket ApplyPassPacket(NetworkReader reader)
        {
            return new PassPacket()
            {
                HasCriticalData = true
            };
        }
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

    public interface IDurakPlayer : ICardPlayer
    {
        DurakActivity LastActivity { get; set; }
        event Action<IDurakPlayer, AttackPacket> OnPlayerTryAttackEvent;
        event Action<IDurakPlayer, DefendPacket> OnPlayerTryDefendEvent;
        event Action<IDurakPlayer, TakePacket> OnPlayerTryTakeEvent;
        event Action<IDurakPlayer, TossPacket> OnPlayerTryTossEvent;
        event Action<IDurakPlayer, TransferPacket> OnPlayerTryTransferEvent;
        event Action<IDurakPlayer, PassPacket> OnPlayerTryPassEvent;
    }
}
