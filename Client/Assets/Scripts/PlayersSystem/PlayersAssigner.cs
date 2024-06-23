using System;
using System.Collections;
using System.Collections.Generic;
using GameSystem;
using MultiplayerSystem.CustomData;
using RoomsSystem;
using UnityEngine;

namespace PlayersSystem
{
    public class PlayersAssigner : MonoBehaviour
    {
        public static PlayersAssigner Singleton { get; set; }

        [SerializeField] private PlayerViewsConnector _playerViewsConnector;

        private NetworkPlayerData[] _playersData = Array.Empty<NetworkPlayerData>();
        private NetworkPlayerData[] _oldPlayersData = Array.Empty<NetworkPlayerData>();

        public int MaxPlayersCount =>  RoomMarker.Singleton.UsersCount;
        
        private void Awake()
        {
            Singleton = this;
            //GlobalEventsContainer.OnPlayerSpawned += AddPlayer;
        }

        //private void OnDisable()
        //    => GlobalEventsContainer.OnPlayerSpawned -= AddPlayer;

        private void Update()
        {
            if (_playersData != _oldPlayersData)
            {
                _oldPlayersData = _playersData;
                //Commit();
            }

            //base.SyncUpdate();
        }

        //public override void AssembleData(Writer writer, byte LOD)
        //{
        //    writer.Write(NetworkConverter.GetConvertedNetworkPlayerDataArrayToByteArray(_playersData));
        //}

        //public override void DisassembleData(Reader reader, byte LOD)
        //{
        //    _playersData = NetworkConverter.GetConvertedByteArrayToNetworkPlayerDataArray(reader.ReadByteArray());
        //    _oldPlayersData = _playersData;
        //}

        private void AddPlayer(PlayerHandler player)
        {
            //if (!Multiplayer.GetUser().IsHost) return;
            List<NetworkPlayerData> tempList = new List<NetworkPlayerData>(_playersData);
            tempList.Add(new NetworkPlayerData(player.Id, (ushort)_playersData.Length));
            _playersData = tempList.ToArray();
            
            if (_playersData.Length != MaxPlayersCount) return;
            //GameManager.Singleton.StartGame();
            StartCoroutine(AssignPlayersRoutine());
        }
        
        //private void AssignPlayersSync()
        //    => _playerViewsConnector.AssignPlayers(_playersData);

        private IEnumerator AssignPlayersRoutine()
        {
            yield return new WaitForSeconds(1f);
            AssignPlayers();
        }
        
        private void AssignPlayers()
        {
            //AssignPlayersSync();
            //InvokeRemoteMethod("AssignPlayersSync");
        }
    }
}