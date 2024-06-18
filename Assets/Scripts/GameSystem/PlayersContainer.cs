using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace GameSystem
{
    public class PlayersContainer : MonoBehaviour
    {
        private TMP_Text _text;

        public static PlayersContainer Singleton { get; set; }

        //private List<PlayerHandler> _players = new List<PlayerHandler>();

        //public List<PlayerHandler> Players => _players;

        private void Awake()
            => Singleton = this;

        //private void OnEnable()
        //    => GlobalEventsContainer.OnPlayerSpawned += AddPlayer;

        //private void OnDisable()
        //    => GlobalEventsContainer.OnPlayerSpawned -= AddPlayer;

        //private void AddPlayer(PlayerHandler playerHandler)
        //    => _players.Add(playerHandler);

        //public PlayerHandler GetPlayerBuyId(int id)
        //{
        //    foreach (var player in _players)
        //    {
        //        if (player.Id != id) continue;
        //        return player;
        //    }

        //    throw new Exception("Can't find player with id " + id);
        //}

        //public PlayerHandler GetDefendingPlayer()
        //{
        //    foreach (var player in _players)
        //        if (player.IsDefending)
        //            return player;
        //    return null;
        //}

        //public PlayerHandler GetMovingPlayer()
        //{
        //    foreach (var player in _players)
        //        if (player.IsMoving)
        //            return player;
        //    throw new Exception("There is no moving player");
        //}

        //public bool AllPayersPass()
        //{
        //    foreach (var player in _players)
        //        if (!player.PassPressed && !player.IsDefending && !player.IsWin)
        //            return false;
        //    return true;
        //}
    }
}