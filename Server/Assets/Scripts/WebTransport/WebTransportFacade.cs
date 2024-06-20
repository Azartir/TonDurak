using Mirror;
using Mirror.SimpleWeb;
using System;
using UnityEngine;

namespace TonDurakServer.WebTransport
{
    public class WebTransportFacade : IWebTransport
    {
        private ushort _port;
        private readonly SimpleWebTransport _simpleWebTransport;

        public event Action<int> OnPlayerConnectedEvent;
        public event Action<int> OnPlayerDisconnectedEvent;
        public event Action<int, TransportError, string> OnServerErrorEvent;

        public WebTransportFacade(ushort port, SimpleWebTransport simpleWebTransport) 
        {
            _port = port;
            _simpleWebTransport = simpleWebTransport;
            Initialize();
        }

        public void StartServer()
        {
            _simpleWebTransport.ServerStart();
            Debug.Log($"Server started on port {_simpleWebTransport.port}");
        }

        public void StopServer()
        {
            _simpleWebTransport.ServerStop();
            Unsubscribe();
        }

        public void SendData(ArraySegment<byte> segment, int channelId)
        {
            _simpleWebTransport.ClientSend(segment, channelId);
        }

        private void Initialize()
        {
            _simpleWebTransport.port = _port;
            Subscribe();
        }

        private void Subscribe()
        {
            _simpleWebTransport.OnServerConnected += OnPlayerConnected;
            _simpleWebTransport.OnServerDisconnected += OnPlayerDisconnected;
            _simpleWebTransport.OnServerError += OnServerError; 
        }

        private void Unsubscribe()
        {
            _simpleWebTransport.OnServerConnected -= OnPlayerConnected;
            _simpleWebTransport.OnServerDisconnected -= OnPlayerDisconnected;
            _simpleWebTransport.OnServerError -= OnServerError;
        }

        private void OnPlayerConnected(int id) => OnPlayerConnectedEvent?.Invoke(id);
        private void OnPlayerDisconnected(int id) => OnPlayerDisconnectedEvent?.Invoke(id);
        private void OnServerError(int code, TransportError error, string message) => OnServerErrorEvent?.Invoke(code, error, message);
    }

    public interface IWebTransport
    {
        void StartServer();
        void StopServer();
        void SendData(ArraySegment<byte> segment, int channelId);
        event Action<int> OnPlayerConnectedEvent;
        event Action<int> OnPlayerDisconnectedEvent;
        event Action<int, TransportError, string> OnServerErrorEvent;
    }
}
