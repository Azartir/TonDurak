using Mirror;
using System;
using System.Collections;
using TonDurakServer.WebTransport;
using UnityEngine;

namespace TonDurakServer.Game
{
    public class ServerGameController : MonoBehaviour
    {
        private IServerGame _serverGame;
        private IWebTransport _webTransport;

        private NetworkWriter _writer = new NetworkWriter();
        private NetworkReader _reader = new NetworkReader(null);
        
        public void InitializeServerGame(IServerGame game, IWebTransport webTransport)
        {
            _serverGame = game;
            _webTransport = webTransport;

            Subscribe();

            _serverGame.StartGame();
        }

        private void Subscribe()
        {
            _serverGame.OnGameStartedEvent += OnGameStarted;
            _serverGame.OnGameErrorEvent += OnGameError;
            _webTransport.OnDataEvent += ApplyData;
        }

        private void Unsubscribe()
        {
            _serverGame.OnGameStartedEvent -= OnGameStarted;
            _serverGame.OnGameErrorEvent -= OnGameError;
            _webTransport.OnDataEvent -= ApplyData;
        }

        private void ApplyData(int id, ArraySegment<byte> data)
        { 
            _reader.SetBuffer(data);
            _serverGame.ApplyPacket(id, _reader);
        }

        private IEnumerator ProceedGame()
        {
            while (_serverGame.Status == GameStatus.Running)
            {
                _serverGame.Tick();

                foreach (var player in _serverGame.ServerPlayers)
                {
                    _writer.Reset();
                    _serverGame.PreparePlayerPacket(player.Id, _writer);
                    _webTransport.SendData(player.Id, _writer);
                }

                yield return null;
            }
        }

        private void OnGameStarted()
        {
            StartCoroutine(ProceedGame());
        }

        private void OnGameError(Exception e)
        {
            Debug.LogError(e);
        }
    }
}
