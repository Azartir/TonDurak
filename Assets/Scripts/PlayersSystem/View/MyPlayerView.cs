
//using MultiplayerSystem.CustomData;
//using UnityEngine;

//namespace PlayersSystem.View
//{
//    public class MyPlayerView : BasePlayerView
//    {
//        [SerializeField] private PlayerCardsView _playerCardsView;
//        [SerializeField] private PlayerActionsView _playerActionsView;
//        [SerializeField] private PlayerButtonsView _playerButtonsView;

//        public override void Init(PlayerHandler playerHandler)
//        {
//            base.Init(playerHandler);
//            _playerCardsView.Init(playerHandler);
//            _playerActionsView.Init(playerHandler);
//            _playerButtonsView.Init(playerHandler);
//        }

//        public override void DisplayCards(NetworkCardData[] cards)
//            => _playerCardsView.DisplayCards(cards);
//    }
//}