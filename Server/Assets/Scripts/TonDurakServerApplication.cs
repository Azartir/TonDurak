using TonDurakServer.Game;
using TonDurakServer.WebTransport;
using UnityEngine;

namespace TonDurakServer
{
    public class TonDurakServerApplication : MonoBehaviour
    {
        [SerializeField]
        private string _serverId;
        [SerializeField]
        private string _ip;
        [SerializeField]
        private ushort _port;
        [SerializeField]
        private byte _maxPlayers;
        [SerializeField]
        private int _maxMessagesPerTick = 10000;
        [SerializeField]
        private bool _noDelay = true;
        [SerializeField]
        private int _sendTimeout = 5000;
        [SerializeField]
        private int _recieveTimeout = 20000;
        [SerializeField]
        private int _handShakeMaxSize = 3000;
        [SerializeField]
        private int _maxMessageSize = 16384;

        private IWebTransport _webTransport;
        private IServerGame _serverGame;
        private ServerGameController _controller;

        private void Awake()
        {
            InitializeServer();
            InitializeGame();
            Subscribe();
        }

        private void InitializeServer()
        {
            var webTransport = gameObject.AddComponent<ServerWebTransport>();
            webTransport.Port = _port;
            webTransport.MaxMessagesPerTick = _maxMessagesPerTick;
            webTransport.SendTimeout = _sendTimeout;
            webTransport.RecieveTimeout = _recieveTimeout;
            webTransport.NoDelay = _noDelay;
            webTransport.HandShakeMaxSize = _handShakeMaxSize;
            webTransport.MaxMessageSize = _maxMessageSize;

            _webTransport = webTransport;
            _webTransport.StartServer();
        }

        private void InitializeGame()
        {
            _serverGame = new ServerDurakGame();
        }

        private void InitializeController()
        {
            _controller = gameObject.AddComponent<ServerGameController>();
            _controller.InitializeServerGame(_serverGame, _webTransport);
        }

        private void Subscribe()
        {
            _webTransport.OnPlayerConnectedEvent += OnClientConnected;
            _webTransport.OnPlayerDisconnectedEvent += OnClientDisconnected;
        }

        private void Unsubscribe()
        {
            _webTransport.OnPlayerConnectedEvent -= OnClientConnected;
            _webTransport.OnPlayerDisconnectedEvent -= OnClientDisconnected;
        }

        private void OnClientConnected(int id)
        {
            if (_serverGame.Status == GameStatus.Prepare)
            {
                _serverGame.AddPlayer(id);
                Debug.Log($"Player with id:{id} connected!");

                if (_serverGame.PlayersCount == _maxPlayers)
                {
                    InitializeController();
                    Debug.Log("GAME STARTED!");
                }
            }
        }

        private void OnClientDisconnected(int id)
        {
            _serverGame.RemovePlayer(id);
            Debug.Log($"Player with id:{id} disconnected!");
        }


        private void OnDestroy()
        {
            Unsubscribe();
            _webTransport.StopServer();
        }
    }
}
