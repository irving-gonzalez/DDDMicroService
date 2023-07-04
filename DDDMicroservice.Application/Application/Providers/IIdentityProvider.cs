using System.Net.Http.Headers;
using DDDMicroservice.Application.Configuration;
using DDDMicroservice.Application.Providers.Models;
using DDDMicroservice.Infrastructure.Clients;

namespace DDDMicroservice.Application.Providers
{
    public interface IIdentityProvider
    {
        Task<List<User>> FindUserById(string id);
    }

    public class KeycloakIdentityProvider : IIdentityProvider
    {
        private readonly KeycloakOptions _options;
        private readonly IRestClient _restClient;
        public KeycloakIdentityProvider(IRestClient restClient, KeycloakOptions options)
        {
            _options = options;

            _restClient = restClient;
            _restClient.SetBaseAddress(new Uri(_options.BaseUrl));
            _restClient.ClearHeaders();
            _restClient.AddHeader("Accept", "Application/json");
            _restClient.AddHeader("User-Agent", "PostmanRuntime/7.29.0");
        }

        private string GetToken()
        {
            var url = $"/auth/realms/retail/protocol/openid-connect/token";

            var model = new TokenRequest
            {
                ClientId = "admin-cli",
                Username = _options.Username,
                Password = _options.Password,
                GrantType = "password"
            };

            var requestOptions = new RequestOptions<TokenRequest>
            {
                Endpoint = url,
                Content = model,
                HttpContentType = HttpContentType.FormUrlEncoded
            };

            var result = _restClient.Post<TokenResponse, TokenRequest>(requestOptions).Result;
            return result.AccessToken;
        }


        public async Task<List<User>> FindUserById(string id)
        {
            var url = $"/auth/realms/retail/custom-resources/users/entityId/{id}";
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }

            var requestOptions = new RequestOptions
            {
                Endpoint = url,
                Authenticator = new BearerAuthenticator(GetToken)
            };

            return await _restClient.Get<List<User>>(requestOptions);
        }
    }
}