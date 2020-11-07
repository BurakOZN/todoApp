using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Panel.Helper
{
    public interface IApiConnection
    {
        ApiConnection AddHeader(Dictionary<string, string> headers);
        Task<ConnectionResponseModel<T>> Delete<T>(string url) where T : class;
        Task<ConnectionResponseModel<T>> Get<T>(string url) where T : class;
        Task<ConnectionResponseModel<T>> Post<T>(string url, object content = null) where T : class;
        Task<ConnectionResponseModel<T>> Put<T>(string url, object content = null) where T : class;
    }
    public class ApiConnection : IApiConnection
    {
        private readonly HttpClient _client;
        private Dictionary<string, string> _headers;

        public ApiConnection AddHeader(Dictionary<string, string> headers)
        {
            _headers = headers;
            return this;
        }

        private void SetHeader(HttpRequestMessage httpRequestMessage)
        {
            foreach (var header in _headers)
            {
                httpRequestMessage.Headers.Add(header.Key, header.Value);
            }
        }
        public ApiConnection(HttpClient client)
        {
            _client = client;
            _headers = new Dictionary<string, string>();
        }

        public async Task<ConnectionResponseModel<T>> Get<T>(string url) where T : class
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            SetHeader(request);
            return await SendAsync<T>(request);
        }
        public async Task<ConnectionResponseModel<T>> Post<T>(string url, object content = null) where T : class
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            if (content != null)
            {
                var contentString = System.Text.Json.JsonSerializer.Serialize(content);
                request.Content = new StringContent(contentString, Encoding.UTF8, "application/json");
            }
            SetHeader(request);
            return await SendAsync<T>(request);
        }
        public async Task<ConnectionResponseModel<T>> Put<T>(string url, object content = null) where T : class
        {
            var request = new HttpRequestMessage(HttpMethod.Put, url);
            if (content != null)
                request.Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(content));
            SetHeader(request);
            return await SendAsync<T>(request);
        }
        public async Task<ConnectionResponseModel<T>> Delete<T>(string url) where T : class
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, url);
            SetHeader(request);
            return await SendAsync<T>(request);
        }

        private async Task<ConnectionResponseModel<T>> SendAsync<T>(HttpRequestMessage request) where T : class
        {
            HttpResponseMessage response;
            try
            {
                response = await _client.SendAsync(request);
            }
            catch (Exception ex)
            {
                return new ConnectionResponseModel<T>() { IsSuccess = false, ErrorMessage = "Request Error" };
            }

            if (response.IsSuccessStatusCode)
            {
                var stringRead = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<T>(stringRead);
                return new ConnectionResponseModel<T>() { IsSuccess = true, Result = result };
            }
            else
            {
                return new ConnectionResponseModel<T>() { IsSuccess = false, ErrorMessage = "Connection Error" };
            }
        }
    }
    public class ConnectionResponseModel<T>
    {
        public bool IsSuccess { get; set; }
        public T Result { get; set; }
        public string ErrorMessage { get; set; }
    }
}
