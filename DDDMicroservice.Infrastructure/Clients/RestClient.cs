using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
//using Serilog;
using System;
using System.IO;
using DDDMicroservice.Infrastructure.Extensions;
using System.Net;

namespace DDDMicroservice.Infrastructure.Clients
{
    public interface IRestClient
    {
        Uri BaseAddress { get; set; }
        void ClearHeaders();
        void AddHeader(string name, string value);
        void SetBaseAddress(Uri address);
        Task<TResponse> Get<TResponse>(RequestOptions requestOptions);
        Task<Stream> GetStream(RequestOptions requestOptions);
        Task<TResponse> Post<TResponse, TContent>(RequestOptions<TContent> requestOptions);
        Task<TResponse> Put<TResponse, TContent>(RequestOptions<TContent> requestOptions);
    }

    public class RestClient : IRestClient
    {
        //private readonly ILogger _logger = Log.ForContext<RestClient>();
        protected readonly HttpClient _httpClient;
        public Uri BaseAddress { get; set; } = new UriBuilder().Uri;

        private WebProxy Proxy { get; set; } = new WebProxy
        {
            Address = new Uri($"http://104.128.77.5"),
            BypassProxyOnLocal = false,
            UseDefaultCredentials = false,
        };

        public RestClient(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            ClearHeaders();
        }
        public void SetBaseAddress(Uri address)
        {
            _httpClient.BaseAddress = address;
        }

        public void AddHeader(string name, string value)
        {
            _httpClient.DefaultRequestHeaders.Add(name, value);
        }

        public void ClearHeaders()
        {
            _httpClient.DefaultRequestHeaders.Clear();
        }

        public async Task<TResponse> Get<TResponse>(RequestOptions requestOptions)
        {
            var responseMessage = await Get(requestOptions);
            return await responseMessage.Content.ReadAsAsync<TResponse>();
        }

        public async Task<Stream> GetStream(RequestOptions requestOptions)
        {
            var responseMessage = await Get(requestOptions);
            return await responseMessage.Content.ReadAsStreamAsync();
        }

        private async Task<HttpResponseMessage> Get(RequestOptions requestOptions)
        {
            if (requestOptions.Authenticator != null)
            {
                requestOptions.Authenticator.Authenticate(_httpClient);
            }
            var responseMessage = await _httpClient.GetAsync(requestOptions.Endpoint);

            await CheckResponse(requestOptions, responseMessage);
            return responseMessage;
        }

        public async Task<TResponse> Post<TResponse, TContent>(RequestOptions<TContent> requestOptions)
        {
            if (requestOptions.Authenticator != null)
            {
                requestOptions.Authenticator.Authenticate(_httpClient);
            }

            var formattedContent = FormatHttpContent(requestOptions.Content, requestOptions.HttpContentType);

            var responseMessage = await _httpClient.PostAsync(requestOptions.Endpoint, formattedContent);

            await CheckResponse(requestOptions, responseMessage);
            return await responseMessage.Content.ReadAsAsync<TResponse>();
        }

        public async Task<TResponse> Put<TResponse, TContent>(RequestOptions<TContent> requestOptions)
        {
            if (requestOptions.Authenticator != null)
            {
                requestOptions.Authenticator.Authenticate(_httpClient);
            }

            var formattedContent = FormatHttpContent(requestOptions.Content, requestOptions.HttpContentType);
            var responseMessage = await _httpClient.PutAsync(requestOptions.Endpoint, formattedContent);

            await CheckResponse(requestOptions, responseMessage);
            return await responseMessage.Content.ReadAsAsync<TResponse>();
        }

        private async Task CheckResponse(RequestOptions requestOptions, HttpResponseMessage responseMessage)
        {
            if (requestOptions.ResponseChecker != null)
            {
                await requestOptions.ResponseChecker(responseMessage);
            }
            else
            {
                if (!responseMessage.IsSuccessStatusCode)
                {
                    var msg = await responseMessage.Content.ReadAsStringAsync();
                    throw new HttpRequestException(msg, null, statusCode: responseMessage.StatusCode);
                }
            }
        }

        private HttpContent FormatHttpContent<TContent>(TContent content, HttpContentType httpContentType)
        {
            switch (httpContentType)
            {
                case HttpContentType.ApplicationJson:
                    var json = JsonConvert.SerializeObject(content, Formatting.None, new JsonSerializerSettings
                    {
                        NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore
                    });
                    return new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                case HttpContentType.FormUrlEncoded:
                    return new FormUrlEncodedContent(content.ToDictionary());
                case HttpContentType.MultipartFormDataContent:
                    if (content is MultipartFormDataContent)
                    {
                        return content as MultipartFormDataContent;
                    }
                    else
                    {
                        throw new Exception("content Type must be MultipartFormDataContent");
                    }

                default:
                    return new StringContent("", System.Text.Encoding.UTF8, "application/json");
            }
        }
    }
}