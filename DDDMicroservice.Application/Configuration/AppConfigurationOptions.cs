using DDDMicroservice.Application.Authorization;
using DDDMicroservice.Infrastructure.ServiceBus.Extesions;

namespace DDDMicroservice.Application.Configuration
{
    public class AppConfigurationOptions
    {
        public ServiceBusOptions ServiceBus { get; set; } = new ServiceBusOptions();
        public RecurrentChildJobOptions rebaseAccountsConfigOptions { get; set; } = new RecurrentChildJobOptions();
        public KeycloakOptions keycloakOptions { get; set; } = new KeycloakOptions();
        public HangFireOptions hangFireOptions { get; set; } = new HangFireOptions();
        public IEnumerable<BasicAuthUser> BasicAuthUsers { get; set; } = new List<BasicAuthUser>();
    }
}