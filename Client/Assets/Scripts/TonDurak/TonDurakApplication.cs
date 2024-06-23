using System;
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

        private void Awake()
        {
            InitializeTransport();
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
            
            Subscribe();
            DontDestroyOnLoad(this);
        }

        private void Subscribe()
        {
            _webTransport.OnPlayerConnectedEvent += OnConnectToServer;
            _webTransport.OnPlayerDisconnectedEvent += OnDisconnectFromServer;
            _webTransport.OnClientErrorEvent += OnClientErrorEvent;
        }

        private void Unsubscribe()
        {
            _webTransport.OnPlayerConnectedEvent -= OnConnectToServer;
            _webTransport.OnPlayerDisconnectedEvent -= OnDisconnectFromServer;
            _webTransport.OnClientErrorEvent -= OnClientErrorEvent;
        }

        private void OnConnectToServer()
        {
            Debug.Log("Player CONNECTED!");
        }

        private void OnDisconnectFromServer()
        {
            Debug.Log("Player DISCONNECTED!");
        }

        private void OnClientErrorEvent(Exception e)
        {
            Debug.Log($"error : {e.Message}");
        }

        private void OnDestroy()
        {
            Unsubscribe();
        }
    }
}
