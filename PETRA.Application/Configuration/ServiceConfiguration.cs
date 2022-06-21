 using System.Reflection;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PETRA.Infrastructure.BackgroundWorker;
using PETRA.Infrastructure.DataAccess.Extesions;
using PETRA.Infrastructure.ServiceBus.Extesions;

namespace PETRA.Application.Configuration
{
    public static class ConfigureServices
    {
        public static void AddApplicationServices(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            services.AddDataAccess(configurationManager.GetConnectionString("UserManager"));
            services.AddBackgroundWorker(configurationManager.GetConnectionString("Hangfire"));
            var appConfigguration = configurationManager.Get<AppConfigurationOptions>();
            services.AddServiceBus(appConfigguration.ServiceBus);           
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}