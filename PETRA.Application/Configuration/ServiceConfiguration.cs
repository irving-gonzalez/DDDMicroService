using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using PETRA.Infrastructure.BackgroundWorker;
using PETRA.Infrastructure.DataAccess.Extesions;
using PETRA.Infrastructure.ServiceBus.Extesions;
using PETRA.Infrastructure.Mediator.Extensions;

namespace PETRA.Application.Configuration
{
    public static class ConfigureServices
    {
        public static void AddApplicationServices(this ConfigureHostBuilder host, ConfigurationManager configurationManager)
        {
            host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureServices((ctx, services )=> 
            {
                services.AddDataAccess(configurationManager.GetConnectionString("UserManager"));
                services.AddBackgroundWorker(configurationManager.GetConnectionString("Hangfire"));
                var appConfigguration = configurationManager.Get<AppConfigurationOptions>();
                services.AddServiceBus(appConfigguration.ServiceBus);           
                services.AddMediatR(Assembly.GetExecutingAssembly());
            })  
            .ConfigureContainer<ContainerBuilder>(( ctx , container) =>
            {
                container.AddOutOfBoundDecorator();
            });
        }
    }
}