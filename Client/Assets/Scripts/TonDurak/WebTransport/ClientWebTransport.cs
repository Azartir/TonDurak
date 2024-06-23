using Mirror;
using Mirror.SimpleWeb;
using System;
using UnityEngine;

namespace TonDurakServer.WebTransport
{
    public class ClientWebTransport : MonoBehaviour, IWebTransport
    {
        private SimpleWebClient _simpleWebClient;

        public event Action OnPlayerConnectedEvent;
        public event Action OnPlayerDisconnectedEvent;
        public event Action<Exception> OnClientErrorEvent;

        private void OnEnable()
        {
            var tcpConfig = new TcpConfig(true, 5000, 20000);

            _simpleWebClient = SimpleWebClient.Create(ushort.MaxValue, 1000, tcpConfig);
            Initialize();
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

        public void SendData(ArraySegment<byte> segment, int channelId)
        {
            _simpleWebClient.Send(segment);
        }

        private void Initialize()
        {
            Subscribe();
        }

        private void Subscribe()
        {
            _simpleWebClient.onConnect += OnPlayerConnected;
            _simpleWebClient.onDisconnect += OnPlayerDisconnected;
            _simpleWebClient.onError += OnClientError;
        }

        private void Unsubscribe()
        {
            _simpleWebClient.onConnect -= OnPlayerConnected;
            _simpleWebClient.onDisconnect -= OnPlayerDisconnected;
            _simpleWebClient.onError -= OnClientError;
        }

        private void OnPlayerConnected() => OnPlayerConnectedEvent?.Invoke();
        private void OnPlayerDisconnected() => OnPlayerDisconnectedEvent?.Invoke();
        private void OnClientError(Exception e) => OnClientErrorEvent?.Invoke(e);
    }

    public interface IWebTransport
    {
        void Connect(string hostname, ushort port);
        void Disconnect();
        void SendData(ArraySegment<byte> segment, int channelId);
        event Action OnPlayerConnectedEvent;
        event Action OnPlayerDisconnectedEvent;
        event Action<Exception> OnClientErrorEvent;
    }
}
