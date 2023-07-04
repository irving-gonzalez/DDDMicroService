using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace DDDMicroservice.Infrastructure.Clients
{
    public interface IAuthenticator
    {
        void Authenticate(HttpClient httpContext);
    }

    public class BearerAuthenticator : IAuthenticator
    {

        //private readonly ILogger _logger = Log.ForContext<BearerAuthenticator>();
        private Func<string> _getToken;
        public BearerAuthenticator(Func<string> getToken)
        {
            _getToken = getToken;
        }

        public void Authenticate(HttpClient httpClient)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _getToken());
            }
            catch (Exception ex)
            {
                // _logger.Error(ex, $"Bearer Authorization failed: {ex.Message}");
                // throw new Exception("Bearer Authorization failed.");
            }
        }
    }
}