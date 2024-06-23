//using System.Collections.Generic;
//using Events;
//using GameSystem.View;
//using PlayersSystem;
//using UnityEngine;

//namespace GameSystem
//{
//    public class WinHandler : MonoBehaviour
//    {
//        [SerializeField] private WinView _view;
//        private PlayerHandler _player;

//        [SerializeField] private List<PlayerHandler> _winningPlayers = new List<PlayerHandler>();

//        private void OnEnable()
//        {
//            GlobalEventsContainer.OnMyPlayerSpawned += AssignPlayer;
//            GlobalEventsContainer.OnPlayerWin += AddWinPlayer;
//            GlobalEventsContainer.OnGameEnd += HandleWin;
//        }

//        private void OnDisable()
//        {
//            GlobalEventsContainer.OnMyPlayerSpawned -= AssignPlayer;
//            GlobalEventsContainer.OnPlayerWin -= AddWinPlayer;
//            GlobalEventsContainer.OnGameEnd -= HandleWin;
//        }

//        private void AssignPlayer(PlayerHandler player)
//            => _player = player;

//        private void AddWinPlayer(PlayerHandler playerHandler)
//        {
//            _winningPlayers.Add(playerHandler);
//            if(_player.IsWin)
//                _view.Init(_player.IsWin);
//            GameManager.Singleton.RemovePlayer(playerHandler);
//        }
        
//        private void HandleWin()
//        {
//            if(_player == null) return;
//            _view.Init(_player.IsWin);
//        }
//    }
//}