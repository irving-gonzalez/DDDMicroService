using Microsoft.Extensions.Configuration;

namespace PETRA.Application.Configuration
{
    public static class AppConfigurationManager
    {
        public static ConfigurationManager Build(string environment)
        {
            var configurationManager = new ConfigurationManager();

            configurationManager
                .AddJsonFile($"appsettings.json",optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json",optional: true, reloadOnChange: true);
            return configurationManager;
        }
    }
}