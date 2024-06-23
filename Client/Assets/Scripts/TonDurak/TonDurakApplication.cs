using System;
using TonDurakClient;
using TonDurakClient.Game;
using TonDurakServer.WebTransport;
using UnityEngine;

namespace TonDurak
{
    public class TonDurakApplication : MonoBehaviour
    {
        [SerializeField]
        private string _hostName;
        [SerializeField]
        private ushort _port;
        [SerializeField]
        private int _maxMessagesPerTick;
        [SerializeField]
        private bool _noDelay;
        [SerializeField]
        private int _sendTimeout;
        [SerializeField]
        private int _recieveTimeout;
        [SerializeField]
        private int _maxMessageSize;

        private IWebTransport _webTransport;
        private IClientGame _clientGame;

        private void Awake()
        {
            InitializeTransport();
            InitializeGame();
            InitializeController();
        }

        private void InitializeTransport()
        {
            var webTransport = gameObject.AddComponent<ClientWebTransport>();

            webTransport.Port = _port;
            webTransport.MaxMessagesPerTick = _maxMessagesPerTick;
            webTransport.SendTimeout = _sendTimeout;
            webTransport.RecieveTimeout = _recieveTimeout;
            webTransport.NoDelay = _noDelay;
            webTransport.MaxMessageSize = _maxMessageSize;

            webTransport.Initialize();
            _webTransport = webTransport;
            _webTransport.Connect(_hostName, _port);
            
            DontDestroyOnLoad(this);
        }

        private void InitializeController()
        {
            var controller = gameObject.AddComponent<ClientGameController>();
            controller.InitializeClientGame(_clientGame, _webTransport);
        }

        private void InitializeGame()
        {
            var game = new ClientDurakGame();
            _clientGame = game;
        }      

        private void OnDestroy()
        {
            
        }
    }
}
