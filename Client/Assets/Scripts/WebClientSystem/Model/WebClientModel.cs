using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebClientSystem
{
    public class WebClientModel
    {
        private readonly string _url;
        private readonly HttpClient _httpClient;

        public WebClientModel(string url)
        {
            _url = url;
            _httpClient = new HttpClient();
        }

        public Task<HttpResponseMessage> InitGetRequest(IWebAction webAction)
        {
            return _httpClient.GetAsync(_url + webAction.Action);
        }

        public Task<HttpResponseMessage> InitPostRequest(IWebAction webAction, string postData)
        {
            var content = new StringContent(postData, Encoding.UTF8, "application/json");
            return _httpClient.PostAsync(_url + webAction.Action, content);
        }
    }
}
