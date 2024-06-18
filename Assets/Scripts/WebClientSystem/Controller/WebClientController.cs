using System.Net.Http;
using System.Threading.Tasks;

namespace WebClientSystem
{
    public class WebClientController
    {
        private WebClientModel _model;

        public WebClientController(string url)
        {
            _model = new WebClientModel(url);
        }

        public Task<HttpResponseMessage> InitGetRequest(IWebAction action)
        {
            return _model.InitGetRequest(action);
        }

        public Task<HttpResponseMessage> InitPostRequest(IWebAction action, string postData)
        {
            return _model.InitPostRequest(action, postData);
        }
    }
}
