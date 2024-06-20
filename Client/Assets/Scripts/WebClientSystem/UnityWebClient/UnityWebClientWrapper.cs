using System.Collections;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;
using WebClientSystem;

namespace UnityWebClient
{
    public class UnityWebClientWrapper : MonoBehaviour
    {
        [SerializeField]
        private string _url;

        private IWebClient _webClient;

        private void Awake()
        {
            _webClient = new WebClient(_url);
        }

        public void InitGetRequest(IWebAction webAction)
        {
            var task = _webClient.InitGetRequest(webAction);
            StartCoroutine(HandleRequest(task, webAction));
        }

        public void InitPostRequest(IWebAction webAction, string postData)
        {
            var task = _webClient.InitPostRequest(webAction, postData);
            StartCoroutine(HandleRequest(task, webAction));
        }

        private IEnumerator HandleRequest(Task<HttpResponseMessage> responseTask, IWebAction webAction)
        {
            while (!responseTask.IsCompleted)
                yield return null;

            if (responseTask.Exception != null)
            {
                webAction.OnFail?.Invoke(responseTask.Exception.Message);
                Debug.Log(responseTask.Exception.Message);
                yield break;
            }

            if (responseTask.Result == null)
            {
                webAction.OnFail?.Invoke(responseTask.Exception.Message);
                Debug.Log(responseTask.Exception.Message);
                yield break;
            }

            Task<string> readTask = responseTask.Result.Content.ReadAsStringAsync();

            while (!readTask.IsCompleted)
                yield return null;


            webAction.OnSuccess?.Invoke(readTask.Result);
            Debug.Log(readTask.Result);
        }
    }
}
