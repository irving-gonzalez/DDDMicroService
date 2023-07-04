using Microsoft.Extensions.Configuration;

namespace DDDMicroservice.Application.Configuration
{
    public static class AppConfigurationManager
    {
        public static ConfigurationManager Build(string environment)
        {
            var configurationManager = new ConfigurationManager();

            configurationManager
                .AddJsonFile($"appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true);

            //EnvironmentVariables override everything.
            configurationManager.AddEnvironmentVariables();

            return configurationManager;
        }
    }
}