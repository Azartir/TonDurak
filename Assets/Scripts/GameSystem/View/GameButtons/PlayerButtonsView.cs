//using System.Collections.Generic;
//using Events;
//using PlayersSystem;
//using UnityEngine;

//namespace GameSystem.View.GameButtons
//{
//    public class PlayerButtonsView : MonoBehaviour
//    {
//        [SerializeField] private List<BaseGameButton> _buttons = new List<BaseGameButton>();

//        private PlayerHandler _playerHandler;

//        private void OnEnable()
//        {
//            GameEventsContainer.OnTableCardPlaced += HandleTableCardPlaced;
//            GameEventsContainer.OnAllTableCardsBit += HandleTableFull;
//            GameEventsContainer.ShouldDisplayPassAfterTake += HandlePlayerPressTake;
//        }

//        private void OnDisable()
//        {
//            GameEventsContainer.OnTableCardPlaced -= HandleTableCardPlaced;
//            GameEventsContainer.OnAllTableCardsBit -= HandleTableFull;
//            GameEventsContainer.ShouldDisplayPassAfterTake -= HandlePlayerPressTake;
//        }

//        public void Init(PlayerHandler playerHandler)
//            => _playerHandler = playerHandler;

//        private void HandleTableCardPlaced()
//        {
//            if (_playerHandler.IsDefending)
//                DisplayButton(GameButtonType.Take);
//            else
//                DisplayButton(GameButtonType.None);
//        }

//        private void HandleTableFull()
//        {
//            if (_playerHandler.IsMoving || _playerHandler.IsThrows)
//                DisplayButton(GameButtonType.Pass);
//            else
//                DisplayButton(GameButtonType.None);
//        }

//        private void HandlePlayerPressTake()
//        {
//            if (!_playerHandler.IsMoving && !_playerHandler.IsThrows) return;
//            DisplayButton(GameButtonType.Pass);
//        }

//        private void DisplayButton(GameButtonType type)
//        {
//            foreach (var button in _buttons)
//                button.TryActivate(type);
//        }
//    }
//}