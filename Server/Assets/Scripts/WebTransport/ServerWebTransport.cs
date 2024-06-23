using Mirror.SimpleWeb;
using System;
using UnityEngine;

namespace TonDurakServer.WebTransport
{
    public class ServerWebTransport : MonoBehaviour, IWebTransport
    {
        private SimpleWebServer _simpleWebServer;

        private SslConfig _sslConfig;
        private TcpConfig _tcpConfig;
        public int MaxMessagesPerTick = 10000;
        public bool NoDelay = true;
        public int SendTimeout = 5000;
        public int RecieveTimeout = 20000;
        public int HandShakeMaxSize = 3000;
        public int MaxMessageSize = 16384;
        public ushort Port;

        public event Action<int> OnPlayerConnectedEvent;
        public event Action<int> OnPlayerDisconnectedEvent;
        public event Action<int, Exception> OnServerErrorEvent;
        public event Action<int, ArraySegment<byte>> OnDataEvent;


        private void Update()
        {
            _simpleWebServer?.ProcessMessageQueue(this);
        }

        public void StartServer()
        {
            _sslConfig = new SslConfig(false, string.Empty, string.Empty, System.Security.Authentication.SslProtocols.None);
            _tcpConfig = new TcpConfig(NoDelay, SendTimeout, RecieveTimeout);

            _simpleWebServer = new SimpleWebServer(
                MaxMessagesPerTick,
                _tcpConfig,
                MaxMessageSize, 
                HandShakeMaxSize,
                _sslConfig);

            Initialize();

            _simpleWebServer.Start(Port);

            Debug.Log($"Server started on port {Port}");
        }

        public void StopServer()
        {
            _simpleWebServer.Stop();
            Unsubscribe();
        }

        public void SendData(int connectionId, ArraySegment<byte> segment)
        {
            _simpleWebServer.SendOne(connectionId, segment);
        }

        public void RecievData()
        {

        }

        private void Initialize()
        {
            Subscribe();
        }

        private void Subscribe()
        {
            _simpleWebServer.onConnect += OnConnected;
            _simpleWebServer.onDisconnect += OnDisconnected;
            _simpleWebServer.onError += OnServerError;
            _simpleWebServer.onData += OnData;
        }

        private void Unsubscribe()
        {
            _simpleWebServer.onConnect -= OnConnected;
            _simpleWebServer.onDisconnect -= OnDisconnected;
            _simpleWebServer.onError -= OnServerError;
        }

        private void OnConnected(int id) => OnPlayerConnectedEvent?.Invoke(id);
        private void OnDisconnected(int id) => OnPlayerDisconnectedEvent?.Invoke(id);
        private void OnServerError(int code, Exception e) => OnServerErrorEvent?.Invoke(code, e);
        private void OnData(int id, ArraySegment<byte> data) => OnDataEvent?.Invoke(id, data);
    }

    public interface IWebTransport
    {
        void StartServer();
        void StopServer();
        void SendData(int connectionId, ArraySegment<byte> segment);
        event Action<int> OnPlayerConnectedEvent;
        event Action<int> OnPlayerDisconnectedEvent;
        event Action<int, ArraySegment<byte>> OnDataEvent;
        event Action<int, Exception> OnServerErrorEvent;
    }
}
