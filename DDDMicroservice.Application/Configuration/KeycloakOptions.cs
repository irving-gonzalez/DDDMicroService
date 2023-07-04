using DDDMicroservice.Infrastructure.ServiceBus.Extesions;

namespace DDDMicroservice.Application.Configuration
{
    public class KeycloakOptions
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string BaseUrl { get; set; } = string.Empty;
    }
}