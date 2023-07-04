using System.Linq;
using System.Security.Claims;

namespace DDDMicroservice.Application.Authorization
{
    public interface IUserAuthService
    {
        bool TryAuthenticate(out ClaimsPrincipal principal, string authenticationType, string username, string password);
    }

    public class InMemoryUserAuthService : IUserAuthService
    {
        private readonly BasicAuthSchemeOptions _authOptions;

        public InMemoryUserAuthService(BasicAuthSchemeOptions authOptions)
        {
            _authOptions = authOptions;
        }

        public bool TryAuthenticate(out ClaimsPrincipal principal, string authenticationType, string username, string password)
        {
            principal = null;

            var matchedUser = _authOptions.Users.FirstOrDefault(user => user.Username == username && user.Password == password);

            if (matchedUser != null)
            {
                var claims = new Claim[]
                {
                    //TODO rename JWTClaims and think about specific roles for BasicAuthUsers
                    new Claim("role", "Admin")
                };

                principal = new ClaimsPrincipal(new ClaimsIdentity(claims, authenticationType));
                return true;
            }

            return false;
        }
    }
}