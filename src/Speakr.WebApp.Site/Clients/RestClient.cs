using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Speakr.WebApp.Site.Clients
{
    public abstract class RestClient
    {
        protected static HttpClient _httpClient;
        private static string _baseUrl;

        public RestClient(string baseUrl)
        {
            _httpClient = new HttpClient();
            _baseUrl = baseUrl;
        }

        public T Get<T>(string uri)
        {
            return GetAsync<T>(uri).Result;
        }

        public HttpResponseMessage Post(string uri, dynamic body)
        {
            return PostAsync(uri, body).Result;
        }

        public async Task<T> GetAsync<T>(string uri)
        {
            var responseMessage = await _httpClient.GetAsync($"{_baseUrl}/{uri}");
            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
                return JsonConvert.DeserializeObject<T>(await responseMessage.Content.ReadAsStringAsync());
            return default(T);
        }

        public async Task<HttpResponseMessage> PostAsync(string uri, dynamic body)
        {
            var jsonObject = JsonConvert.SerializeObject(body);
            var content = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");
            return await _httpClient.PostAsync($"{_baseUrl}/{uri}", content);
        }
    }
}
