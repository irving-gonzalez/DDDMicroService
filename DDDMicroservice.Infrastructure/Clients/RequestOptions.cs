using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace DDDMicroservice.Infrastructure.Clients
{
    public class RequestOptions
    {
        public string Endpoint { get; set; } = string.Empty;
        public HttpContentType HttpContentType { get; set; } = HttpContentType.ApplicationJson;
        public IAuthenticator Authenticator;
        /// <summary>
        /// Custom function to check an HTTP response and throw an exception if needed.
        /// </summary>
        /// <value></value>
        public Func<HttpResponseMessage, Task> ResponseChecker { get; set; } = null;
    }

    public class RequestOptions<T> : RequestOptions
    {
        public T Content { get; set; }
    }
}