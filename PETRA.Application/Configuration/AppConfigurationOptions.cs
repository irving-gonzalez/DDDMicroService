using PETRA.Infrastructure.ServiceBus.Extesions;

namespace PETRA.Application.Configuration
{
    public class AppConfigurationOptions
    {
        public ServiceBusOptions ServiceBus { get; set; } = new ServiceBusOptions();
        public string Test { get; set; } = string.Empty;
    }
}