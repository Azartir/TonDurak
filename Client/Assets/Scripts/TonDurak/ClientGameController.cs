using Mirror;
using System;
using System.Collections;
using TonDurakServer.WebTransport;
using UnityEngine;

namespace TonDurakClient.Game
{
    public  class ClientGameController : MonoBehaviour
    {
        private IClientGame _clienGame;
        private IWebTransport _webTransport;

        private NetworkWriter _writer = new NetworkWriter();
        private NetworkReader _reader = new NetworkReader(null);

        public void InitializeClientGame(IClientGame game, IWebTransport webTransport)
        {
            _clienGame = game;
            _webTransport = webTransport;

            Subscribe();
        }

        private void Subscribe()
        {
            _webTransport.OnPlayerConnectedEvent += OnConnect;
            _webTransport.OnPlayerDisconnectedEvent += OnDisconnect;
            _webTransport.OnClientErrorEvent += OnException;
            _webTransport.OnDataEvent += ApplyData;
        }

        private void Unsubscribe()
        {
            _webTransport.OnPlayerConnectedEvent -= OnConnect;
            _webTransport.OnPlayerDisconnectedEvent -= OnDisconnect;
            _webTransport.OnClientErrorEvent -= OnException;
            _webTransport.OnDataEvent -= ApplyData;
        }

        private IEnumerator ProceedGame()
        {
            while (_clienGame.Status == GameStatus.Running)
            {
                _clienGame.Tick();

                foreach (var player in _clienGame.ClientPlayers)
                {
                    _writer.Reset();
                    player.Serialize(_writer);
                    _webTransport.SendData(_writer);
                }

                yield return null;
            }
        }

        private void OnConnect()
        {
            Debug.Log("CONNECTED!");
            StartCoroutine(ProceedGame());
        }

        private void OnDisconnect()
        {
            Debug.Log("DISCONNECTED!");
            StopAllCoroutines();
        }

        private void ApplyData(ArraySegment<byte> data)
        {
            _reader.SetBuffer(data);
            _clienGame.Deserialize(_reader);
        }

        private void OnException(Exception e)
        {
            Debug.Log($"{e.Message}");
        }

        private void OnDestroy()
        {
            Unsubscribe();
        }
    }
}
