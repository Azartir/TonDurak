using Mirror.SimpleWeb;
using System;
using UnityEngine;

namespace TonDurakServer.WebTransport
{
    public class ClientWebTransport : MonoBehaviour, IWebTransport
    {
        private SimpleWebClient _simpleWebClient;

        private SslConfig _sslConfig;
        private TcpConfig _tcpConfig;
        public int MaxMessagesPerTick = 10000;
        public bool NoDelay = true;
        public int SendTimeout = 5000;
        public int RecieveTimeout = 20000;
        public int MaxMessageSize = 16384;
        public ushort Port;

        public event Action OnPlayerConnectedEvent;
        public event Action OnPlayerDisconnectedEvent;
        public event Action<ArraySegment<byte>> OnDataEvent;
        public event Action<Exception> OnClientErrorEvent;

        public void Initialize()
        {
            _tcpConfig = new TcpConfig(NoDelay, SendTimeout, RecieveTimeout);
            _simpleWebClient = SimpleWebClient.Create(MaxMessageSize, MaxMessagesPerTick, _tcpConfig);

            Subscribe();
        }

        public void Connect(string hostname, ushort port)
        {
            var uri = new Uri($"ws://{hostname}:{port}");
            _simpleWebClient.Connect(uri);
            Debug.Log(_simpleWebClient.ConnectionState);
        }

        private void Update()
        {
            _simpleWebClient?.ProcessMessageQueue(this);
        }

        public void Disconnect()
        {
            _simpleWebClient.Disconnect();
            Unsubscribe();
        }

        public void SendData(ArraySegment<byte> segment)
        {
            _simpleWebClient.Send(segment);
        } 

        private void Subscribe()
        {
            _simpleWebClient.onConnect += OnConnected;
            _simpleWebClient.onDisconnect += OnDisconnected;
            _simpleWebClient.onData += OnData;
            _simpleWebClient.onError += OnClientError;
        }

        private void Unsubscribe()
        {
            _simpleWebClient.onConnect -= OnConnected;
            _simpleWebClient.onDisconnect -= OnDisconnected;
            _simpleWebClient.onData -= OnData;
            _simpleWebClient.onError -= OnClientError;
        }

        private void OnConnected() => OnPlayerConnectedEvent?.Invoke();
        private void OnDisconnected() => OnPlayerDisconnectedEvent?.Invoke();
        private void OnData(ArraySegment<byte> data) => OnDataEvent?.Invoke(data);
        private void OnClientError(Exception e) => OnClientErrorEvent?.Invoke(e);

        private void OnDestroy()
        {
            Unsubscribe();
        }
    }

    public interface IWebTransport
    {
        void Connect(string hostname, ushort port);
        void Disconnect();
        void SendData(ArraySegment<byte> segment);
        event Action OnPlayerConnectedEvent;
        event Action OnPlayerDisconnectedEvent;
        event Action<ArraySegment<byte>> OnDataEvent;
        event Action<Exception> OnClientErrorEvent;
    }
}
