using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text;
using System.Text.Encodings.Web;


namespace DDDMicroservice.Application.Authorization
{
    public class BasicAuthHandler : AuthenticationHandler<BasicAuthSchemeOptions>
    {
        private ClaimsPrincipal principal;
        private AuthenticationHeaderValue headerValues;
        private readonly IUserAuthService _userAuthService;
        public BasicAuthHandler(IUserAuthService userAuthService, IOptionsMonitor<BasicAuthSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
            _userAuthService = userAuthService;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return Task.FromResult<AuthenticateResult>(AuthenticateResult.NoResult());
            }

            if (!AuthenticationHeaderValue.TryParse(Request.Headers["Authorization"], out headerValues))
            {
                return Task.FromResult<AuthenticateResult>(AuthenticateResult.NoResult());
            }

            if (headerValues.Scheme != BasicAuthDefaults.BasicAuthScheme)
            {
                return Task.FromResult<AuthenticateResult>(AuthenticateResult.NoResult());
            }

            if (!TryDecodeCredentials(headerValues.Parameter, out string user, out string password))
            {
                return Task.FromResult<AuthenticateResult>(AuthenticateResult.NoResult());
            }

            if (!_userAuthService.TryAuthenticate(out principal, Scheme.Name, user, password))
            {
                return Task.FromResult<AuthenticateResult>(AuthenticateResult.Fail(new System.Exception("invalid username or password")));
            }

            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            return Task.FromResult<AuthenticateResult>(AuthenticateResult.Success(ticket));
        }

        protected override Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            Response.Headers["WWW-Authenticate"] = $"Basic realm=\"BBWA\", charset=\"UTF-8\"";
            Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            return Task.FromResult(0);
        }

        private bool TryDecodeCredentials(string token, out string user, out string password)
        {
            user = string.Empty;
            password = string.Empty;

            try
            {
                var bytes = Convert.FromBase64String(headerValues.Parameter);
                var decodedString = Encoding.UTF8.GetString(bytes);
                string[] credentials = decodedString.Split(':');
                user = credentials[0];
                password = credentials[1];

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}