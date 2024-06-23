using Mirror;
using Mirror.SimpleWeb;
using System;
using TonDurakServer.WebTransport;
using UnityEditor.VersionControl;
using UnityEngine;

namespace TonDurak
{
    public class TonDurakApplication : MonoBehaviour
    {
        private IWebTransport _webTransport;

        private void Awake()
        {
            _webTransport = gameObject.AddComponent<ClientWebTransport>();
            Subscribe();
            _webTransport.Connect("localhost", 7778);
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
