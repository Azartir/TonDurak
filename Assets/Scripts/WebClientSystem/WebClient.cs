using System.Net.Http;
using System.Threading.Tasks;

namespace WebClientSystem
{
    public class WebClient : IWebClient
    {
        private WebClientController _controller;

        public WebClient(string url)
        {
            _controller = new WebClientController(url);
        }

        public Task<HttpResponseMessage> InitGetRequest(IWebAction action)
        {
            return _controller.InitGetRequest(action);
        }

        public Task<HttpResponseMessage> InitPostRequest(IWebAction action, string postData)
        {
            return _controller.InitPostRequest(action, postData);
        }
    }

    public interface IWebClient
    {
        Task<HttpResponseMessage> InitGetRequest(IWebAction action);
        Task<HttpResponseMessage> InitPostRequest(IWebAction action, string postData);
    }
}
