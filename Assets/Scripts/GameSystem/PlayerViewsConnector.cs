using System;
using System.Collections.Generic;
using Events;
using MultiplayerSystem.CustomData;
using UnityEngine;

namespace GameSystem
{
    public class PlayerViewsConnector : MonoBehaviour
    {
        //[SerializeField] private BasePlayerView _myPlayerView;
        //[SerializeField] private List<BasePlayerView> _otherPlayersView;

        //public void AssignPlayers(NetworkPlayerData[] playersData)
        //{
        //    if (PlayersAssigner.Singleton.MaxPlayersCount > 2)
        //    {
        //        (_otherPlayersView[0], _otherPlayersView[1]) = (_otherPlayersView[1], _otherPlayersView[0]);
        //    }
        //    var playersHandlers = GetFixedPlayersList(playersData);
        //    int myPlayerId = GetMyPlayerId(playersHandlers);
        //    playersHandlers[myPlayerId].AssignView(_myPlayerView);
        //    var myPlayer = playersHandlers[myPlayerId];
        //    AssignOtherPlayersView(myPlayerId, playersHandlers, myPlayer);
        //}

        //private List<PlayerHandler> GetFixedPlayersList(NetworkPlayerData[] playersData)
        //{
        //    var playersHandlers = new List<PlayerHandler>();
        //    foreach (var player in playersData)
        //        playersHandlers.Add(PlayersContainer.Singleton.GetPlayerBuyId(player.Id));
        //    return playersHandlers;
        //}

        //private int GetMyPlayerId(List<PlayerHandler> players)
        //{
        //    for (int i = 0; i < players.Count; i++)
        //    {
        //        if (!players[i].IsMine) continue;
        //        return i;
        //    }

        //    throw new Exception("There is no player with IsMine param");
        //}

        //private void AssignOtherPlayersView(int myPlayerIndex, List<PlayerHandler> players, PlayerHandler myPlayer)
        //{
        //    var fixedPlayersSequence = GetFixedSequenceList(players, myPlayerIndex);
        //    var currentViewIndex = 0;
        //    for (int i = 1; i < fixedPlayersSequence.Count; i++)
        //    {
        //        fixedPlayersSequence[i].AssignView(_otherPlayersView[currentViewIndex]);
        //        currentViewIndex++;
        //    }

        //    GlobalEventsContainer.OnPlayersViewAssign?.Invoke(fixedPlayersSequence);
        //}

        //private List<PlayerHandler> GetFixedSequenceList(List<PlayerHandler> list, int index)
        //{
        //    if (index == -1)
        //    {
        //        Debug.LogError("Element not found in the list");
        //        return list;
        //    }

        //    List<PlayerHandler> rotatedList = new List<PlayerHandler>();

        //    for (int i = index; i < list.Count; i++)
        //    {
        //        rotatedList.Add(list[i]);
        //    }

        //    for (int i = 0; i < index; i++)
        //    {
        //        rotatedList.Add(list[i]);
        //    }

        //    return rotatedList;
        //}
    }
}