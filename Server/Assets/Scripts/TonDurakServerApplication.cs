using Mirror.SimpleWeb;
using TonDurakServer.WebTransport;
using UnityEngine;

namespace TonDurakServer
{
    public class TonDurakServerApplication : MonoBehaviour
    {
        [SerializeField]
        private string _backendUrl;
        [SerializeField]
        private string _ip;
        [SerializeField]
        private ushort _port;

        private IWebTransport _webTransport;

        private void Awake()
        {
            InitializeServer();
        }

        private void InitializeServer()
        {
            var transport = gameObject.AddComponent<SimpleWebTransport>();
            _webTransport = new WebTransportFacade(_port, transport);
            _webTransport.StartServer();
        }

        private void Subscribe()
        {

        }

        private void Unsubscribe()
        {

        }


        private void OnDestroy()
        {
            Unsubscribe();
        }
    }
}
