using PETRA.Infrastructure.ServiceBus.Extesions;

namespace PETRA.API.Config
{
    public class AppConfiguration
    {
        public ServiceBusOptions ServiceBus { get; set; } = new ServiceBusOptions();
        public string Test { get; set; } = string.Empty;
    }
}