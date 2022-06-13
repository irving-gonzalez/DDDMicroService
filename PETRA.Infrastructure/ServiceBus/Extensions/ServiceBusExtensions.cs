using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace PETRA.Infrastructure.ServiceBus.Extesions
{
    public class ServiceBusOptions
    {
        public string Host { get; set; } = string.Empty;
        public string Port { get; set; } = "5672";
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public static class ServiceBusExtensions
    {
        public static void AddServiceBus(this IServiceCollection services, ServiceBusOptions configOptions)
        {
            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, cfg) =>
                    {
                        cfg.Host(configOptions.Host, ushort.Parse(configOptions.Port) ,"/", h =>
                        {
                            h.Username("guest");
                            h.Password("guest");
                        });
                    });
            });
           
             // OPTIONAL, but can be used to configure the bus options
            services.AddOptions<MassTransitHostOptions>()
                .Configure(options =>
                {
                    // if specified, waits until the bus is started before
                    // returning from IHostedService.StartAsync
                    // default is false
                    options.WaitUntilStarted = true;

                    // if specified, limits the wait time when starting the bus
                    options.StartTimeout = TimeSpan.FromSeconds(10);

                    // if specified, limits the wait time when stopping the bus
                    options.StopTimeout = TimeSpan.FromSeconds(30);
                });
        }
    }
}